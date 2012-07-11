exec('
create view EmailDto
as
select 
	e.Id as EmailId
from Email e
')