@echo off
for /f "tokens=2 delims==" %%a in ('wmic OS Get localdatetime /value') do set "dt=%%a"
set "YY=%dt:~2,2%" & set "YYYY=%dt:~0,4%" & set "MM=%dt:~4,2%" & set "DD=%dt:~6,2%"
set "HH=%dt:~8,2%" & set "Min=%dt:~10,2%" & set "Sec=%dt:~12,2%"
set "fullstamp=ef_migration_%YYYY%-%MM%-%DD%_%HH%-%Min%-%Sec%.sql"

REM This script will create EFvmigration script in ..\SQL directory

@echo on
dotnet ef migrations script --idempotent --project ..\..\FunkyCode.Blog.Inf.EntityFramework --startup-project ..\..\FunkyCode.Blog.WebApi --verbose --output .\Output\%fullstamp%
pause


