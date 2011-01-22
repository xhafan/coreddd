use master;

DECLARE @dbname nvarchar(128)
SET @dbname = N'EmailMakerTest'

IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
begin
	ALTER DATABASE EmailMakerTest
	SET Single_USER
	WITH ROLLBACK IMMEDIATE;

	drop database EmailMakerTest
end




create database EmailMakerTest
go
use EmailMakerTest;  
go
