using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.Dtos.Emails
{
    public class EmailPartDtoAutoMap : IAutoMappingOverride<EmailPartDto>
    {
        public void Override(AutoMapping<EmailPartDto> mapping)
        {
            mapping.CompositeId()
                .KeyProperty(x => x.EmailId)
                .KeyProperty(x => x.PartId);
        }
    }
}