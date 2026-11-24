using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ABPS_WEB.Infrastructure.Data;

/// <summary>
/// Strips DateTimeKind on the way to and from PostgreSQL `timestamp without time zone` columns.
/// See AbpsContext.ConfigureConventions for why: Npgsql rejects Kind=Utc for that column type,
/// while SQL Server's `datetime` simply stored the wall-clock value regardless of Kind.
/// </summary>
public sealed class UnspecifiedKindDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UnspecifiedKindDateTimeConverter()
        : base(
            v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified),
            v => DateTime.SpecifyKind(v, DateTimeKind.Unspecified))
    {
    }
}
