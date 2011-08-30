exec('
CREATE view [dbo].[vw_GetAllEmailTemplates]
as
select 
Id as EmailTemplateId,
Name,
UserId
from EmailTemplate
')


