# ABPS_WEB - fresh install

| Part | Where | From this package |
|---|---|---|
| Frontend | Cloudflare Pages -> `abps.sivayaantechnologies.com` | `fe-cloudflare\` |
| Backend API | VM, IIS, port 6011 (tunnel `abpsapi.sivayaantechnologies.com` -> `http://localhost:6011`) | `api\` |
| Database | VM, PostgreSQL `localhost:5432`, database `abps`, login `abpsadmin` | `db\` |

The database starts with master data only: rooms (all Available), room types, floors, statuses,
booking categories, donation types, payment types, transaction types, SMS templates, occasions,
Telugu calendar, countries/states/districts, user roles, and one user: **Admin / 123**.
No bookings, donations, donors, transactions or executive members.

## 1. VM prerequisites (once)

1. **PostgreSQL 18** - https://www.postgresql.org/download/windows/ (keep port 5432, note the `postgres` password).
2. **ASP.NET Core 10 Hosting Bundle** - https://dotnet.microsoft.com/download/dotnet/10.0 ->
   "ASP.NET Core Runtime 10.x" -> *Hosting Bundle*. Then run `iisreset`.
3. IIS installed; cloudflared tunnel publishing `abpsapi.sivayaantechnologies.com` -> `http://localhost:6011`.

## 2. Backend + database (one command)

Copy this whole folder to the VM, open **PowerShell as Administrator** in it:

```
powershell -ExecutionPolicy Bypass -File .\deploy-vm.ps1
```

Asks once for the `postgres` password, then creates the database, loads master data, deploys the
API to `C:\server\abpsapi`, binds IIS to port 6011 and checks `localhost:6011` + the public URL.
Re-running it later only redeploys the API - an existing database is never reloaded.

### Database steps by hand (what the script runs)

```
cd db
"C:\Program Files\PostgreSQL\18\bin\psql.exe" -h localhost -U postgres  -d postgres -v ON_ERROR_STOP=1 -v db_password=<app password> -f 01_create_role_and_database.sql
"C:\Program Files\PostgreSQL\18\bin\psql.exe" -h localhost -U abpsadmin -d abps     -v ON_ERROR_STOP=1 -f 02_schema.sql
"C:\Program Files\PostgreSQL\18\bin\psql.exe" -h localhost -U abpsadmin -d abps     -v ON_ERROR_STOP=1 --single-transaction -f 03_master_data.sql
```

Passwords: `postgres` = yours. `<app password>` / `abpsadmin` = the `Password=` value of
`ConnectionStrings:AbpsContext` in `api\appsettings.Production.json` (kept out of git on purpose).
Run 03 only once (it inserts rows).

## 3. Frontend (Cloudflare Pages)

Workers & Pages -> Create -> **Pages** (not Worker) -> *Upload assets* -> upload the
`fe-cloudflare` folder -> add custom domain `abps.sivayaantechnologies.com`.
`_redirects` inside the folder handles page refreshes on deep links.

## 4. After install

Open https://abps.sivayaantechnologies.com, sign in as **Admin / 123**, then immediately change the
password and create staff users in **Admin -> User Registration**.
