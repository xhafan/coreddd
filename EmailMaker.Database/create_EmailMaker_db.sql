use master;

DECLARE @dbname nvarchar(128)
SET @dbname = N'EmailMaker'

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
begin
	ALTER DATABASE EmailMaker
	SET Single_USER
	WITH ROLLBACK IMMEDIATE;

	drop database EmailMaker
end




create database EmailMaker
go
use EmailMaker;  
go    
