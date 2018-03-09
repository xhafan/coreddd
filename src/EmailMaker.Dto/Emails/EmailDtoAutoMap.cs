using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Dtos.Emails
{
    public class EmailDtoAutoMap : IAutoMappingOverride<EmailDto>
    {
        public void Override(AutoMapping<EmailDto> mapping)
        {
            mapping.Id(x => x.EmailId);
            mapping.IgnoreProperty(x => x.Parts);
        }
    }
}