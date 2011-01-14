using EmailMaker.Domain.EmailTemplates;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class HtmlEmailTemplatePartMap : SubclassMap<HtmlEmailTemplatePart>
    {
        public HtmlEmailTemplatePartMap()
        {
            KeyColumn("Id");
            DiscriminatorValue("H");
            Map(x => x.Html);
        }
    }
}