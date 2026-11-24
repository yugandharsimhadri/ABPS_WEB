-- ABPS_WEB fresh install, step 1 of 3: application login role + empty database.
-- Run as the PostgreSQL superuser, connected to the "postgres" database, passing the application
-- password (the Password= value in api\appsettings.Production.json) as a psql variable:
--   psql -h localhost -U postgres -d postgres -v ON_ERROR_STOP=1 -v db_password=<password> -f 01_create_role_and_database.sql
-- Safe to re-run: existing role/database are left as they are.

SELECT format('CREATE ROLE abpsadmin LOGIN PASSWORD %L', :'db_password')
WHERE NOT EXISTS (SELECT 1 FROM pg_roles WHERE rolname = 'abpsadmin')
\gexec

SELECT 'CREATE DATABASE abps OWNER abpsadmin ENCODING ''UTF8'' TEMPLATE template0'
WHERE NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'abps')
\gexec
