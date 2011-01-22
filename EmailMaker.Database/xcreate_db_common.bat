@echo off
IF (%DBNAME%)==() GOTO ERROR

type create_%DBNAME%_db.sql > temp_create_%DBNAME%_db.sql
type transaction_start.sql >> temp_create_%DBNAME%_db.sql
type EmailMaker_generated_database_schema.sql >> temp_create_%DBNAME%_db.sql

powershell -Command "Get-ChildItem . -filter data\common\*.sql | get-content" >> temp_create_%DBNAME%_db.sql
IF (%SKIPAPPDATA%)==(YES) GOTO SKIPAPPDATA
powershell -Command "Get-ChildItem . -filter data\app\*.sql | get-content" >> temp_create_%DBNAME%_db.sql
:SKIPAPPDATA

type transaction_end.sql >> temp_create_%DBNAME%_db.sql

sqlcmd -U emailmaker -P password01 -i temp_create_%DBNAME%_db.sql
rem del temp_create_%DBNAME%_db.sql
GOTO END

:ERROR
echo error: DBNAME variable not defined, exiting...
:END
pause