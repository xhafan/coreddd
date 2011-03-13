using EmailMaker.DTO.EmailTemplate;
using FluentNHibernate.Mapping;

namespace EmailMaker.DTO.NHibernateMappings
{
    public class EmailTemplatePartDTOMap : ClassMap<EmailTemplatePartDTO>
    {
        public EmailTemplatePartDTOMap()
        {
            Table("vw_EmailTemplatePart");
            CompositeId()
                .KeyProperty(x => x.EmailTemplateId)
                .KeyProperty(x => x.PartId);
            Map(x => x.EmailTemplatePartType);
            Map(x => x.Html);
            Map(x => x.VariableValue);
        }
    }
}