using System.Linq;
using Core.Queries;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetExistingRecipientsQuery : BaseNHibernateCriteriaQueryMessageHandler<GetExistingRecipientsQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetExistingRecipientsQueryMessage message)
        {
            // todo: implemente XLOCK on the sql and write concurrent locking test for it
            return Session.QueryOver<Recipient>()
                .WhereRestrictionOn(x => x.EmailAddress).IsIn(message.RecipientEmailAddresses.ToArray())
                .UnderlyingCriteria;
        }
    }
}