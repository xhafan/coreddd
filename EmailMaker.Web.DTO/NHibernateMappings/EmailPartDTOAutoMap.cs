using EmailMaker.DTO.Emails;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailPartDTOAutoMap : IAutoMappingOverride<EmailPartDTO>
    {
        public void Override(AutoMapping<EmailPartDTO> mapping)
        {
            mapping.CompositeId()
                .KeyProperty(x => x.EmailId)
                .KeyProperty(x => x.PartId);
        }
    }
}