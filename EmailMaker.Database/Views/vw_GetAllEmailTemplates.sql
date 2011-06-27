exec('
CREATE view [dbo].[vw_GetAllEmailTemplates]
as
select etc.Id, etc.Name, etc.Culture, et.Id as EmailTemplateId
from EmailTemplate et 
join EmailTemplateForCulture etc on etc.EmailTemplateId = et.Id
')


