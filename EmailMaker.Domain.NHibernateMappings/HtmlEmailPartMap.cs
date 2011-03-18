using EmailMaker.Domain.Emails;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class HtmlEmailPartMap : SubclassMap<HtmlEmailPart>
    {
        public HtmlEmailPartMap()
        {
            KeyColumn("Id");
            Map(x => x.Html).Length(10000);
        }
    }
}