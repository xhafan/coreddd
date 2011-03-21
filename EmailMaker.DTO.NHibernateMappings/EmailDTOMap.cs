using EmailMaker.DTO.Emails;
using FluentNHibernate.Mapping;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailDTOMap : ClassMap<EmailDTO>
    {
        public EmailDTOMap()
        {
            Table("vw_Email");
            Id(x => x.EmailId);
        }
    }
}