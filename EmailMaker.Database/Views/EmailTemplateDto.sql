exec('
create view EmailTemplateDto
as
select 
	Id as EmailTemplateId
	, Name
from EmailTemplate
')