using EmailMaker.DTO.EmailTemplates;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailTemplateDTOAutoMap : IAutoMappingOverride<EmailTemplateDTO>
    {
        public void Override(AutoMapping<EmailTemplateDTO> mapping)
        {
            mapping.Id(x => x.EmailTemplateId);
            mapping.IgnoreProperty(x => x.Parts);
        }
    }
}