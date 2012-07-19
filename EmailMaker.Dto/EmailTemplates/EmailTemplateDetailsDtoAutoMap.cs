using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDetailsDtoAutoMap : IAutoMappingOverride<EmailTemplateDetailsDto>
    {
        public void Override(AutoMapping<EmailTemplateDetailsDto> mapping)
        {
            mapping.Id(x => x.EmailTemplateId);
        }
    }
}