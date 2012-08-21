IF OBJECT_ID(N'EmailTemplatePartDto') IS NOT NULL
BEGIN
DROP VIEW EmailTemplatePartDto
END
GO

create view EmailTemplatePartDto
as
select 
	et.Id as EmailTemplateId,
	case when hetp.EmailTemplatePartId is not null then 'Html' else 'Variable' end as PartType,
	etp.Id as PartId,
	hetp.Html,
	vetp.Value as VariableValue
from EmailTemplate et
join EmailTemplatePart etp on etp.EmailTemplateId = et.Id
left join HtmlEmailTemplatePart hetp on hetp.EmailTemplatePartId = etp.Id
left join VariableEmailTemplatePart vetp on vetp.EmailTemplatePartId = etp.Id

go
