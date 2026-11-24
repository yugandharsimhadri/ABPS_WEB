// ABPS_WEB.DataMigrator — copies every table of the app's EF model from the legacy SQL Server
// database into PostgreSQL.
//
// Usage:
//   ABPS_WEB.DataMigrator --source "<SQL Server connection string>"
//                         --target "<PostgreSQL connection string>" [--truncate]
//
// The PostgreSQL schema must already exist (restore publish/db/abps_postgres.sql, or run
// `dotnet ef database update`). Table and column names come from the EF model — the schema the app
// actually uses — so only columns that exist on both sides are copied (differences are reported).
// After loading: identity sequences are advanced past the copied keys, every foreign key is checked
// for orphan rows, and source/target row counts are compared. Non-zero exit code on any mismatch.

using System.Globalization;
using System.Text.RegularExpressions;
using ABPS_WEB.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;

string? Arg(string name) => args.SkipWhile(a => a != name).Skip(1).FirstOrDefault();
var source = Arg("--source");
var target = Arg("--target");
var truncate = args.Contains("--truncate");
if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
{
    Console.Error.WriteLine("Usage: ABPS_WEB.DataMigrator --source \"<SQL Server conn>\" --target \"<PostgreSQL conn>\" [--truncate]");
    return 2;
}

// ---- 1. Model metadata (tables, columns, store types, foreign keys) ----
using var ctx = new AbpsContext(new DbContextOptionsBuilder<AbpsContext>().UseNpgsql(target).Options);
var relational = ctx.GetService<IDesignTimeModel>().Model.GetRelationalModel();
var tables = TopologicalOrder(relational.Tables.Where(t => t.Name != "__EFMigrationsHistory").ToList());

// ---- 2. Source introspection ----
await using var src = new SqlConnection(source);
await src.OpenAsync();
var srcColumns = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
await using (var cmd = new SqlCommand(
    "SELECT TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'dbo'", src))
await using (var r = await cmd.ExecuteReaderAsync())
{
    while (await r.ReadAsync())
    {
        if (!srcColumns.TryGetValue(r.GetString(0), out var set))
            srcColumns[r.GetString(0)] = set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        set.Add(r.GetString(1));
    }
}

var modelTableNames = new HashSet<string>(tables.Select(t => t.Name), StringComparer.OrdinalIgnoreCase);
foreach (var extra in srcColumns.Keys.Where(k => !modelTableNames.Contains(k)).OrderBy(k => k))
    Console.WriteLine($"  [skip] source table '{extra}' is not used by the app model — not migrated");

// ---- 3. Load ----
await using var dst = new NpgsqlConnection(target);
await dst.OpenAsync();
await using var tx = await dst.BeginTransactionAsync();

var isSuper = (bool)(await new NpgsqlCommand("SELECT rolsuper FROM pg_roles WHERE rolname = current_user", dst, tx).ExecuteScalarAsync())!;
if (isSuper)
    // Defers FK enforcement during the bulk load (tables are also loaded parent-first as a fallback).
    await new NpgsqlCommand("SET LOCAL session_replication_role = replica", dst, tx).ExecuteNonQueryAsync();

if (truncate)
{
    var all = string.Join(", ", tables.Select(Q));
    await new NpgsqlCommand($"TRUNCATE TABLE {all} RESTART IDENTITY CASCADE", dst, tx).ExecuteNonQueryAsync();
}
else
{
    foreach (var t in tables)
    {
        var n = (long)(await new NpgsqlCommand($"SELECT COUNT(*) FROM {Q(t)}", dst, tx).ExecuteScalarAsync())!;
        if (n > 0)
        {
            Console.Error.WriteLine($"Target table {t.Name} already has {n} rows. Re-run with --truncate to replace target data.");
            return 3;
        }
    }
}

var nulStripped = 0;
var results = new List<(string Table, long Source, long Copied)>();
foreach (var t in tables)
{
    if (!srcColumns.TryGetValue(t.Name, out var available))
    {
        Console.WriteLine($"  [warn] table '{t.Name}' does not exist in source — left empty");
        results.Add((t.Name, 0, 0));
        continue;
    }

    var cols = t.Columns.Where(c => available.Contains(c.Name)).ToList();
    foreach (var missing in t.Columns.Where(c => !available.Contains(c.Name)))
        Console.WriteLine($"  [warn] {t.Name}.{missing.Name} not in source — takes its default/NULL");

    var pgTypes = cols.Select(c => BaseType(c.StoreType)).ToList();
    // Source count first — the copy reader below holds the (non-MARS) connection until disposed.
    long sourceCount;
    await using (var count = new SqlCommand($"SELECT COUNT_BIG(*) FROM [dbo].[{t.Name}]", src) { CommandTimeout = 0 })
        sourceCount = (long)(await count.ExecuteScalarAsync())!;

    long copied;
    await using (var read = new SqlCommand(
        $"SELECT {string.Join(", ", cols.Select(c => $"[{c.Name}]"))} FROM [dbo].[{t.Name}]", src) { CommandTimeout = 0 })
    await using (var reader = await read.ExecuteReaderAsync())
    await using (var writer = await dst.BeginBinaryImportAsync(
        $"COPY {Q(t)} ({string.Join(", ", cols.Select(c => $"\"{c.Name}\""))}) FROM STDIN (FORMAT BINARY)"))
    {
        while (await reader.ReadAsync())
        {
            await writer.StartRowAsync();
            for (var i = 0; i < cols.Count; i++)
            {
                var raw = reader.GetValue(i);
                if (raw is DBNull) { await writer.WriteNullAsync(); continue; }
                await WriteTyped(writer, ConvertValue(raw, pgTypes[i], ref nulStripped), pgTypes[i]);
            }
        }
        copied = (long)await writer.CompleteAsync();
    }

    results.Add((t.Name, sourceCount, copied));
}

// ---- 4. Advance identity sequences past the copied keys ----
var identities = new List<(string Table, string Column)>();
await using (var idCmd = new NpgsqlCommand(
    "SELECT table_name, column_name FROM information_schema.columns WHERE table_schema = 'public' AND is_identity = 'YES'", dst, tx))
await using (var r = await idCmd.ExecuteReaderAsync())
    while (await r.ReadAsync()) identities.Add((r.GetString(0), r.GetString(1)));
foreach (var (table, column) in identities)
    await new NpgsqlCommand(
        $"SELECT setval(pg_get_serial_sequence('\"{table}\"', '{column}'), COALESCE((SELECT MAX(\"{column}\") FROM \"{table}\"), 0) + 1, false)",
        dst, tx).ExecuteScalarAsync();

// ---- 5. Foreign-key orphan check (FKs were not enforced during a superuser load) ----
var orphanProblems = 0;
foreach (var fk in tables.SelectMany(t => t.ForeignKeyConstraints))
{
    var notNull = string.Join(" AND ", fk.Columns.Select(c => $"d.\"{c.Name}\" IS NOT NULL"));
    var join = string.Join(" AND ", fk.Columns.Zip(fk.PrincipalColumns, (c, p) => $"p.\"{p.Name}\" = d.\"{c.Name}\""));
    var orphans = (long)(await new NpgsqlCommand(
        $"SELECT COUNT(*) FROM {Q(fk.Table)} d WHERE {notNull} AND NOT EXISTS (SELECT 1 FROM {Q(fk.PrincipalTable)} p WHERE {join})",
        dst, tx).ExecuteScalarAsync())!;
    if (orphans > 0)
    {
        orphanProblems++;
        Console.WriteLine($"  [FAIL] {orphans} row(s) in {fk.Table.Name} violate FK {fk.Name} -> {fk.PrincipalTable.Name}");
    }
}

if (orphanProblems > 0)
{
    await tx.RollbackAsync();
    Console.Error.WriteLine("Foreign-key violations found — nothing was committed.");
    return 4;
}
await tx.CommitAsync();

// ---- 6. Report ----
Console.WriteLine();
Console.WriteLine($"{"Table",-36} {"Source",10} {"Copied",10}");
var mismatches = 0;
foreach (var (table, s, c) in results)
{
    var flag = s == c ? "" : "  <-- MISMATCH";
    if (s != c) mismatches++;
    Console.WriteLine($"{table,-36} {s,10} {c,10}{flag}");
}
Console.WriteLine($"\nTables: {results.Count}  Rows: {results.Sum(r => r.Copied)}  Identity sequences reset: {identities.Count}  NUL chars stripped: {nulStripped}");
Console.WriteLine(mismatches == 0 ? "OK — all row counts match." : $"{mismatches} table(s) with row-count mismatches.");
return mismatches == 0 ? 0 : 5;

// ---- helpers ----
static string Q(ITable t) => $"\"{t.Schema ?? "public"}\".\"{t.Name}\"";

// "numeric(18,2)" -> "numeric", "timestamp(3) without time zone" -> "timestamp without time zone"
static string BaseType(string storeType) =>
    Regex.Replace(Regex.Replace(storeType, @"\(\s*\d+(\s*,\s*\d+)?\s*\)", ""), @"\s+", " ").Trim().ToLowerInvariant();

static object ConvertValue(object v, string pgType, ref int nulStripped)
{
    switch (pgType)
    {
        case "integer": return Convert.ToInt32(v, CultureInfo.InvariantCulture);
        case "smallint": return Convert.ToInt16(v, CultureInfo.InvariantCulture);
        case "bigint": return Convert.ToInt64(v, CultureInfo.InvariantCulture);
        case "numeric": return Convert.ToDecimal(v, CultureInfo.InvariantCulture);
        case "boolean": return Convert.ToBoolean(v, CultureInfo.InvariantCulture);
        case "real": return Convert.ToSingle(v, CultureInfo.InvariantCulture);
        case "double precision": return Convert.ToDouble(v, CultureInfo.InvariantCulture);
        case "text":
        case "character varying":
        case "character":
            var s = v as string ?? Convert.ToString(v, CultureInfo.InvariantCulture) ?? "";
            if (s.Contains('\0')) { nulStripped++; s = s.Replace("\0", ""); } // PostgreSQL text cannot hold NUL
            return s;
        case "timestamp without time zone":
            return DateTime.SpecifyKind(v is DateTimeOffset o ? o.DateTime : (DateTime)v, DateTimeKind.Unspecified);
        case "date":
            return v is DateTime d ? DateOnly.FromDateTime(d) : v;
        default: return v;
    }
}

static Task WriteTyped(NpgsqlBinaryImporter w, object v, string pgType) => v switch
{
    int x => w.WriteAsync(x, pgType),
    short x => w.WriteAsync(x, pgType),
    long x => w.WriteAsync(x, pgType),
    decimal x => w.WriteAsync(x, pgType),
    bool x => w.WriteAsync(x, pgType),
    float x => w.WriteAsync(x, pgType),
    double x => w.WriteAsync(x, pgType),
    string x => w.WriteAsync(x, pgType),
    DateTime x => w.WriteAsync(x, pgType),
    DateOnly x => w.WriteAsync(x, pgType),
    TimeSpan x => w.WriteAsync(x, pgType),
    Guid x => w.WriteAsync(x, pgType),
    byte[] x => w.WriteAsync(x, pgType),
    _ => w.WriteAsync(v, pgType),
};

// Parents before children so the load also works for a non-superuser (FKs enforced row by row).
static List<ITable> TopologicalOrder(List<ITable> tables)
{
    var ordered = new List<ITable>();
    var visited = new HashSet<ITable>();
    void Visit(ITable t, HashSet<ITable> stack)
    {
        if (visited.Contains(t) || !stack.Add(t)) return; // cycle guard
        foreach (var fk in t.ForeignKeyConstraints)
            if (fk.PrincipalTable != t && tables.Contains(fk.PrincipalTable)) Visit(fk.PrincipalTable, stack);
        stack.Remove(t);
        if (visited.Add(t)) ordered.Add(t);
    }
    foreach (var t in tables.OrderBy(t => t.Name)) Visit(t, new HashSet<ITable>());
    return ordered;
}
