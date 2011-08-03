using EmailMaker.Domain.Emails.EmailStates;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class DraftEmailStateMap : SubclassMap<DraftEmailState>
    {
        public DraftEmailStateMap()
        {
            DiscriminatorValue(EmailState.Draft.Name);
        }
    }
}