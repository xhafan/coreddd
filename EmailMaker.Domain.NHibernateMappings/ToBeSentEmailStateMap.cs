using EmailMaker.Domain.Emails.EmailStates;
using FluentNHibernate.Mapping;

namespace EmailMaker.Domain.NHibernateMappings
{
    public class ToBeSentEmailStateMap : SubclassMap<ToBeSentEmailState>
    {
        public ToBeSentEmailStateMap()
        {
            DiscriminatorValue(EmailState.ToBeSent.Name);
        }    
    }
}