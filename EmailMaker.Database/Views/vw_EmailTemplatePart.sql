exec('
create view vw_EmailTemplatePart
as
select 
et.Id as EmailTemplateId,
case when hetp.Id is not null then ''Html'' else ''Variable'' end as PartType,
etp.Id as PartId,
hetp.Html,
vetp.Value as VariableValue
from EmailTemplate et
join EmailTemplatePart etp on etp.EmailTemplateId = et.Id
left join HtmlEmailTemplatePart hetp on hetp.Id = etp.Id
left join VariableEmailTemplatePart vetp on vetp.Id = etp.Id
')