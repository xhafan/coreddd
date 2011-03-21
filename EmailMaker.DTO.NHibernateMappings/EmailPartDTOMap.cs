using EmailMaker.DTO.Emails;
using FluentNHibernate.Mapping;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailPartDTOMap : ClassMap<EmailPartDTO>
    {
        public EmailPartDTOMap()
        {
            Table("vw_EmailPart");
            CompositeId()
                .KeyProperty(x => x.EmailId)
                .KeyProperty(x => x.PartId);
            Map(x => x.PartType);
            Map(x => x.Html);
            Map(x => x.VariableValue);
        }
    }
}