exec('
CREATE view [dbo].[vw_GetAllEmailTemplates]
as
select et.Id as EmailTemplateId, etc.Name, etc.Culture
from EmailTemplate et 
join EmailTemplateForCulture etc on etc.EmailTemplateId = et.Id
')


