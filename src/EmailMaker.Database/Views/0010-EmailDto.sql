IF OBJECT_ID(N'EmailDto') IS NOT NULL
BEGIN
DROP VIEW EmailDto
END
GO

create view EmailDto
as
select 
	Id as EmailId
from Email 

go