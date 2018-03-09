IF OBJECT_ID(N'EmailTemplateDto') IS NOT NULL
BEGIN
DROP VIEW EmailTemplateDto
END
GO

create view EmailTemplateDto
as
select 
	Id as EmailTemplateId
	, Name
from EmailTemplate

go
