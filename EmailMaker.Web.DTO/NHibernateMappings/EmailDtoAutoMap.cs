using EmailMaker.DTO.Emails;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailDtoAutoMap : IAutoMappingOverride<EmailDTO>
    {
        public void Override(AutoMapping<EmailDTO> mapping)
        {
            mapping.Id(x => x.EmailId);
            mapping.IgnoreProperty(x => x.Parts);
        }
    }
}