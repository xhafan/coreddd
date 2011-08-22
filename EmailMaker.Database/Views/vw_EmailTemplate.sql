exec('
create view vw_EmailTemplate
as
select 
Id as EmailTemplateId,
Name
from EmailTemplate
')