exec('
create view EmailTemplateDetailsDTO
as
select 
	Id as EmailTemplateId,
	Name,
	UserId
from EmailTemplate
')


