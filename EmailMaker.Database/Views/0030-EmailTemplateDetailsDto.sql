IF OBJECT_ID(N'EmailTemplateDetailsDTO') IS NOT NULL
BEGIN
DROP VIEW EmailTemplateDetailsDTO
END
GO

create view EmailTemplateDetailsDTO
as
select 
	Id as EmailTemplateId,
	Name,
	UserId
from EmailTemplate

go
