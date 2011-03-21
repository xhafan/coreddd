exec('
create view vw_Email
as
select 
e.Id as EmailId
from Email e
')