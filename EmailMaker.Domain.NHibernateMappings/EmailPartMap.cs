using EmailMaker.Domain.Emails;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class EmailPartMap : ClassMap<EmailPart>
    {
        public EmailPartMap()
        {
            Id(x => x.Id)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .GeneratedBy.HiLo("100");
            Map(x => x.Position);
        }
    }
}