using EmailMaker.Domain.EmailTemplates;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class EmailTemplatePartMap : ClassMap<EmailTemplatePart>
    {
        public EmailTemplatePartMap()
        {
            Id(x => x.Id)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .GeneratedBy.HiLo("100");
            Map(x => x.Position);
        }
    }
}