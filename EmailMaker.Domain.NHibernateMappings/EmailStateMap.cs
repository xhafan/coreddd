using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class EmailStateMap : ClassMap<EmailState>
    {
        public EmailStateMap()
        {
            Id(x => x.Id)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .GeneratedBy.HiLo("100");

            Map(x => x.Name);
            Map(x => x.CanSend);
        }
    }
}