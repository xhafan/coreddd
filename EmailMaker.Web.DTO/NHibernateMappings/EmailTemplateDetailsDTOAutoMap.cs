using EmailMaker.DTO.EmailTemplates;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailTemplateDetailsDTOAutoMap : IAutoMappingOverride<EmailTemplateDetailsDTO>
    {
        public void Override(AutoMapping<EmailTemplateDetailsDTO> mapping)
        {
            mapping.Id(x => x.EmailTemplateId);
        }
    }
}