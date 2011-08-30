using EmailMaker.DTO.EmailTemplates;
using FluentNHibernate.Mapping;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailTemplateDetailsDTOMap : ClassMap<EmailTemplateDetailsDTO>
    {
        public EmailTemplateDetailsDTOMap()
        {
            Table("vw_GetAllEmailTemplates");
            Id(x => x.EmailTemplateId);
            Map(x => x.Name);
            Map(x => x.UserId);
        }
    }
}