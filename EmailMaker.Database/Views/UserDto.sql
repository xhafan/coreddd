exec('
create view UserDto
as
select 
	Id as UserId
	,FirstName
	,LastName
	,EmailAddress
	,[Password]
from [User]
')