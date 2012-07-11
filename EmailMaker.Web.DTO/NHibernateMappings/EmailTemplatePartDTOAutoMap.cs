using EmailMaker.DTO.EmailTemplates;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailTemplatePartDTOAutoMap : IAutoMappingOverride<EmailTemplatePartDTO>
    {
        public void Override(AutoMapping<EmailTemplatePartDTO> mapping)
        {
            mapping.CompositeId()
                .KeyProperty(x => x.EmailTemplateId)
                .KeyProperty(x => x.PartId);
        }
    }
}