exec('
create view vw_EmailPart
as
select 
e.Id as EmailId,
case when hep.Id is not null then ''Html'' else ''Variable'' end as PartType,
ep.Id as PartId,
hep.Html,
vep.Value as VariableValue
from Email e
join EmailPart ep on ep.EmailId = e.Id
left join HtmlEmailPart hep on hep.Id = ep.Id
left join VariableEmailPart vep on vep.Id = ep.Id
')