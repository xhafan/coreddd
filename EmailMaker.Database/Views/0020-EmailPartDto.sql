IF OBJECT_ID(N'EmailPartDto') IS NOT NULL
BEGIN
DROP VIEW EmailPartDto
END
GO

create view EmailPartDto
as
select 
	e.Id as EmailId,
	case when hep.EmailPartId is not null then 'Html' else 'Variable' end as PartType,
	ep.Id as PartId,
	hep.Html,
	vep.Value as VariableValue
from Email e
join EmailPart ep on ep.EmailId = e.Id
left join HtmlEmailPart hep on hep.EmailPartId = ep.Id
left join VariableEmailPart vep on vep.EmailPartId = ep.Id

go
