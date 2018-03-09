IF OBJECT_ID(N'UserDto') IS NOT NULL
BEGIN
DROP VIEW UserDto
END
GO

create view UserDto
as
select 
	Id as UserId
	,FirstName
	,LastName
	,EmailAddress
	,[Password]
from [User]

go
