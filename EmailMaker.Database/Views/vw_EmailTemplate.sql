exec('
create view vw_EmailTemplate
as
select 
et.Id as EmailTemplateId
from EmailTemplate et
')