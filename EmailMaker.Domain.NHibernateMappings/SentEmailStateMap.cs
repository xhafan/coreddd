using EmailMaker.Domain.Emails.EmailStates;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class SentEmailStateMap : SubclassMap<SentEmailState>
    {
        public SentEmailStateMap()
        {
            DiscriminatorValue(EmailState.Sent.Name);
        }
    }
}