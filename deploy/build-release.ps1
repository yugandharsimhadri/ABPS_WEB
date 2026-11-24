<#
.SYNOPSIS
  Builds the ABPS_WEB fresh-install release into ..\publish:
    publish\fe-cloudflare - static frontend: Cloudflare Pages (direct upload of this folder)
    publish\api           - IIS-ready backend (installed by deploy-vm.ps1)
    publish\db            - PostgreSQL scripts: 01 role+database, 02 schema, 03 master data
    publish\deploy-vm.ps1 - one-shot VM setup: PostgreSQL database + IIS site
    publish\DEPLOY.md     - step-by-step guide
#>
# Continue, not Stop: Windows PowerShell 5.1 turns any native stderr line (e.g. "npm notice") into a
# terminating error. Every native step below checks $LASTEXITCODE explicitly instead.
$ErrorActionPreference = "Continue"
$root = Split-Path -Parent (Split-Path -Parent $MyInvocation.MyCommand.Path)
$out = Join-Path $root "publish"

Remove-Item $out -Recurse -Force -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Force $out | Out-Null

Write-Host "== API (IIS) ==" -ForegroundColor Cyan
dotnet publish (Join-Path $root "src\ABPS_WEB.Api") -c Release -o (Join-Path $out "api")
if ($LASTEXITCODE -ne 0) { throw "API publish failed" }
if (-not (Test-Path (Join-Path $out "api\appsettings.Production.json"))) {
    Write-Warning "appsettings.Production.json is missing (it is git-ignored) - create it in src\ABPS_WEB.Api before deploying."
}

Write-Host "== Frontend (Cloudflare Pages) ==" -ForegroundColor Cyan
Push-Location (Join-Path $root "src\ABPS_WEB.Client")
try { npm run build; if ($LASTEXITCODE -ne 0) { throw "Frontend build failed" } } finally { Pop-Location }
Copy-Item (Join-Path $root "src\ABPS_WEB.Client\dist") (Join-Path $out "fe-cloudflare") -Recurse -ErrorAction Stop

Write-Host "== Database scripts + deployment files ==" -ForegroundColor Cyan
Copy-Item (Join-Path $root "deploy\db") (Join-Path $out "db") -Recurse -ErrorAction Stop
Copy-Item (Join-Path $root "deploy\deploy-vm.ps1") $out -ErrorAction Stop
Copy-Item (Join-Path $root "deploy\DEPLOY.md") $out -ErrorAction Stop

Write-Host "`nRelease ready: $out" -ForegroundColor Green
