<#
.SYNOPSIS
  ABPS_WEB backend - fresh install on the Windows VM: PostgreSQL database + IIS site.

.DESCRIPTION
  Run from the release folder (contains api\ and db\) in an *Administrator* PowerShell:

      powershell -ExecutionPolicy Bypass -File .\deploy-vm.ps1

  1. Checks prerequisites: ASP.NET Core 10 Hosting Bundle, PostgreSQL.
  2. Database (asks once for the PostgreSQL 'postgres' superuser password):
       db\01_create_role_and_database.sql  - role abpsadmin + empty database abps   (as postgres)
       db\02_schema.sql                    - all tables                              (as abpsadmin)
       db\03_master_data.sql               - master/lookup data only, Admin user     (as abpsadmin)
     If the database already contains rooms, steps 02/03 are skipped - re-running this script to
     redeploy the API never touches existing data.
  3. IIS: app pool (No Managed Code, 64-bit), site on http port 6011, files copied to the site
     folder, permissions for the app-pool identity, start.
  4. Health check: http://localhost:6011 and the public tunnel URL.

  Defaults match the existing setup: site folder C:\server\abpsapi, port 6011 (Cloudflare tunnel
  abpsapi.sivayaantechnologies.com -> http://localhost:6011), PostgreSQL localhost:5432, database
  abps, login abpsadmin (same values as api\appsettings.Production.json).
#>
param(
    [string]$SiteName = "abpsapi",
    [string]$SitePath = "C:\server\abpsapi",
    [int]$Port = 6011,
    [string]$PgBin = "",
    [string]$PgHost = "localhost",
    [int]$PgPort = 5432,
    [string]$PgSuperUser = "postgres",
    [switch]$SkipDatabase,
    [switch]$SkipIis
)

$ErrorActionPreference = "Stop"
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$DbName = "abps"; $DbUser = "abpsadmin"   # fixed names, created by db\01_create_role_and_database.sql

function Step($m) { Write-Host "`n=== $m ===" -ForegroundColor Cyan }
function Ok($m)   { Write-Host "  [ok] $m" -ForegroundColor Green }
function Warn($m) { Write-Host "  [!!] $m" -ForegroundColor Yellow }
function Fail($m) { Write-Host "  [FAIL] $m" -ForegroundColor Red; exit 1 }

$principal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Fail "Run this script from an Administrator PowerShell."
}
foreach ($p in @("api\ABPS_WEB.Api.dll", "api\appsettings.Production.json", "db\01_create_role_and_database.sql", "db\02_schema.sql", "db\03_master_data.sql")) {
    if (-not (Test-Path (Join-Path $here $p))) { Fail "Missing $p next to this script - run it from the release folder." }
}

# The application's DB password lives only in api\appsettings.Production.json (never in git).
$connStr = (Get-Content (Join-Path $here "api\appsettings.Production.json") -Raw | ConvertFrom-Json).ConnectionStrings.AbpsContext
$DbPassword = ""
foreach ($part in $connStr.Split(';')) {
    $kv = $part.Split('=', 2)
    if ($kv.Count -eq 2 -and $kv[0].Trim() -eq "Password") { $DbPassword = $kv[1] }
}
if (-not $DbPassword) { Fail "No Password= found in ConnectionStrings:AbpsContext of api\appsettings.Production.json." }

# ---------------------------------------------------------------- 1. prerequisites
Step "Prerequisites"
if (-not $SkipIis) {
    $ancm = "$env:ProgramFiles\IIS\Asp.Net Core Module\V2\aspnetcorev2.dll"
    $runtimes = ""
    try { $runtimes = (& dotnet --list-runtimes) -join "`n" } catch { }
    if (-not (Test-Path $ancm) -or $runtimes -notmatch "Microsoft\.AspNetCore\.App 10\.") {
        Warn "ASP.NET Core 10 Hosting Bundle not found (IIS module present: $(Test-Path $ancm))."
        Warn "This is what causes 'HTTP Error 500.30 / failed to load coreclr'."
        Fail "Install 'ASP.NET Core Runtime 10.x - Windows Hosting Bundle' from https://dotnet.microsoft.com/download/dotnet/10.0, run 'iisreset', then re-run this script."
    }
    Ok "ASP.NET Core 10 Hosting Bundle installed"
}

if (-not $SkipDatabase) {
    if (-not $PgBin) {
        $PgBin = Get-ChildItem "$env:ProgramFiles\PostgreSQL" -Directory -ErrorAction SilentlyContinue |
            Sort-Object { [int]($_.Name -replace '\D', '') } -Descending |
            ForEach-Object { Join-Path $_.FullName "bin" } |
            Where-Object { Test-Path (Join-Path $_ "psql.exe") } | Select-Object -First 1
    }
    if (-not $PgBin -or -not (Test-Path (Join-Path $PgBin "psql.exe"))) {
        Fail "PostgreSQL not found. Install PostgreSQL 18 (https://www.postgresql.org/download/windows/) or pass -PgBin '<path>\bin'."
    }
    Ok "PostgreSQL tools: $PgBin"
}

# ---------------------------------------------------------------- 2. database
if (-not $SkipDatabase) {
    Step "PostgreSQL database '$DbName'"
    $psql = Join-Path $PgBin "psql.exe"
    $sec = Read-Host -AsSecureString "Password for PostgreSQL superuser '$PgSuperUser'"
    $superPw = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($sec))

    # SQL always goes through files: Windows PowerShell 5.1 strips embedded double quotes from
    # native-exe arguments, which would mangle quoted (case-sensitive) PostgreSQL identifiers.
    function RunPsql([string]$user, [string]$pw, [string]$db, [string]$file, [switch]$Atomic, [string[]]$Vars = @()) {
        $env:PGPASSWORD = $pw
        $psqlArgs = @("-h", $PgHost, "-p", $PgPort, "-U", $user, "-d", $db, "-tAq", "-v", "ON_ERROR_STOP=1")
        foreach ($v in $Vars) { $psqlArgs += @("-v", $v) }
        $psqlArgs += @("-f", $file)
        if ($Atomic) { $psqlArgs += "--single-transaction" }
        $prev = $ErrorActionPreference; $ErrorActionPreference = "Continue"   # psql NOTICEs go to stderr
        $out = & $psql @psqlArgs 2>&1
        $code = $LASTEXITCODE; $ErrorActionPreference = $prev
        if ($code -ne 0) { Fail "psql failed ($user@$db, $(Split-Path -Leaf $file)): $($out | Out-String)" }
        return ($out | Where-Object { $_ -isnot [System.Management.Automation.ErrorRecord] } | Out-String).Trim()
    }
    function Sql([string]$sql) {
        $tmp = [IO.Path]::GetTempFileName()
        [IO.File]::WriteAllText($tmp, $sql, (New-Object Text.UTF8Encoding($false)))
        try { return RunPsql $DbUser $DbPassword $DbName $tmp } finally { Remove-Item $tmp -ErrorAction SilentlyContinue }
    }

    RunPsql $PgSuperUser $superPw "postgres" (Join-Path $here "db\01_create_role_and_database.sql") -Vars @("db_password=$DbPassword") | Out-Null
    Ok "Role '$DbUser' and database '$DbName' ready"

    $rooms = 0
    if ((Sql "SELECT to_regclass('public.""tbl_Room""') IS NOT NULL;") -eq "t") {
        $rooms = [int](Sql "SELECT count(*) FROM public.""tbl_Room"";")
    }
    if ($rooms -gt 0) {
        Ok "Database already set up ($rooms rooms) - schema and data left untouched."
    } else {
        RunPsql $DbUser $DbPassword $DbName (Join-Path $here "db\02_schema.sql") | Out-Null
        Ok "Schema created"
        RunPsql $DbUser $DbPassword $DbName (Join-Path $here "db\03_master_data.sql") -Atomic | Out-Null
        Ok "Master data loaded"
    }
    $summary = Sql "SELECT (SELECT count(*) FROM public.""tbl_Room"") || ' rooms, ' || (SELECT count(*) FROM public.""tbl_User"") || ' user(s), ' || (SELECT count(*) FROM public.""tbl_BookingDetails"") || ' bookings';"
    Ok "Database contents: $summary"
}

# ---------------------------------------------------------------- 3. IIS
if (-not $SkipIis) {
    Step "IIS site '$SiteName' on port $Port"
    Import-Module WebAdministration

    $existing = Get-Website | Where-Object { $_.physicalPath -eq $SitePath -or $_.Name -eq $SiteName } | Select-Object -First 1
    if ($existing) { $SiteName = $existing.Name; Ok "Using existing site '$SiteName'" }

    $poolName = if ($existing) { $existing.applicationPool } else { $SiteName }
    if (-not (Test-Path "IIS:\AppPools\$poolName")) { New-WebAppPool -Name $poolName | Out-Null; Ok "Created app pool $poolName" }
    # In-process ASP.NET Core: no .NET Framework CLR, and 64-bit to match the published app.
    Set-ItemProperty "IIS:\AppPools\$poolName" -Name managedRuntimeVersion -Value ""
    Set-ItemProperty "IIS:\AppPools\$poolName" -Name enable32BitAppOnWin64 -Value $false
    Ok "App pool $($poolName): No Managed Code, 64-bit"

    if ((Get-WebAppPoolState -Name $poolName).Value -ne "Stopped") { Stop-WebAppPool -Name $poolName; Start-Sleep 3 }

    New-Item -ItemType Directory -Force $SitePath | Out-Null
    # /MIR keeps the folder an exact copy of the release; the stdout log folder is preserved.
    robocopy (Join-Path $here "api") $SitePath /MIR /XD logs /NFL /NDL /NJH /NJS /NP | Out-Null
    if ($LASTEXITCODE -ge 8) { Fail "Copying API files failed (robocopy exit $LASTEXITCODE)." }
    New-Item -ItemType Directory -Force (Join-Path $SitePath "logs") | Out-Null
    & icacls $SitePath /grant "IIS AppPool\$($poolName):(OI)(CI)M" /T /Q | Out-Null
    Ok "API files deployed to $SitePath"

    if (-not $existing) {
        New-Website -Name $SiteName -PhysicalPath $SitePath -ApplicationPool $poolName -Port $Port | Out-Null
        Ok "Created site $SiteName"
    } else {
        Set-ItemProperty "IIS:\Sites\$SiteName" -Name applicationPool -Value $poolName
        Set-ItemProperty "IIS:\Sites\$SiteName" -Name physicalPath -Value $SitePath
        if (-not (Get-WebBinding -Name $SiteName -Protocol http -Port $Port)) {
            New-WebBinding -Name $SiteName -Protocol http -Port $Port -IPAddress "*" | Out-Null
            Ok "Added http binding on port $Port"
        }
    }

    Start-WebAppPool -Name $poolName
    Start-Website -Name $SiteName
    Ok "Site started"
}

# ---------------------------------------------------------------- 4. health check
if ($SkipIis) { Write-Host "`nDone (database only)." -ForegroundColor Green; exit 0 }
Step "Health check"
function Probe($url) {
    try { $r = Invoke-WebRequest $url -UseBasicParsing -TimeoutSec 60; return [int]$r.StatusCode }
    catch { if ($_.Exception.Response) { return [int]$_.Exception.Response.StatusCode } return 0 }
}
Start-Sleep 2
$local = Probe "http://localhost:$Port/api/lookups"
if ($local -eq 401) {
    Ok "http://localhost:$Port/api/lookups -> 401 (API running; endpoint needs login, as expected)"
} else {
    Warn "http://localhost:$Port/api/lookups -> $local (expected 401)"
    Warn "Recent ASP.NET Core Module errors from the Application event log:"
    Get-EventLog -LogName Application -Source "IIS AspNetCore Module V2" -Newest 5 -ErrorAction SilentlyContinue |
        ForEach-Object { Write-Host "    $($_.TimeGenerated)  $($_.Message)" }
    Warn "For the app's own startup output set stdoutLogEnabled=`"true`" in $SitePath\web.config and check $SitePath\logs."
    exit 1
}
$public = Probe "https://abpsapi.sivayaantechnologies.com/api/lookups"
if ($public -eq 401) { Ok "https://abpsapi.sivayaantechnologies.com/api/lookups -> 401 (tunnel reaches the API)" }
else { Warn "Public URL returned $public - check the Cloudflare tunnel (cloudflared) is running and maps to http://localhost:$Port" }

Write-Host "`nDone. Sign in as Admin / 123 and change the password (Admin > User Registration)." -ForegroundColor Green
