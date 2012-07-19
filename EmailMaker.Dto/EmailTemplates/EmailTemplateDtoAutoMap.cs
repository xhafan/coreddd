using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDtoAutoMap : IAutoMappingOverride<EmailTemplateDto>
    {
        public void Override(AutoMapping<EmailTemplateDto> mapping)
        {
            mapping.Id(x => x.EmailTemplateId);
            mapping.IgnoreProperty(x => x.Parts);
        }
    }
}