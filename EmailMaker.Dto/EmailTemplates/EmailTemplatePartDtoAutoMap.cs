using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplatePartDtoAutoMap : IAutoMappingOverride<EmailTemplatePartDto>
    {
        public void Override(AutoMapping<EmailTemplatePartDto> mapping)
        {
            mapping.CompositeId()
                .KeyProperty(x => x.EmailTemplateId)
                .KeyProperty(x => x.PartId);
        }
    }
}