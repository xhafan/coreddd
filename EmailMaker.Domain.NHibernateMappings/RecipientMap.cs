using EmailMaker.Domain.Emails;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class RecipientMap : ClassMap<Recipient>
    {
        public RecipientMap()
        {
            Id(x => x.Id)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .GeneratedBy.HiLo("100");

            Map(x => x.EmailAddress).Not.Nullable();
            Map(x => x.Name).Length(10000);
        }
    }
}