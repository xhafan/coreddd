using System.Linq;
using CoreDdd.Queries;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetExistingRecipientsQuery : BaseNhibernateCriteriaQueryHandler<Messages.GetExistingRecipientsQuery>
    {
        public override ICriteria GetCriteria<TResult>(Messages.GetExistingRecipientsQuery query)
        {
            // todo: implemente XLOCK on the sql and write concurrent locking test for it
            return Session.QueryOver<Recipient>()
                .WhereRestrictionOn(x => x.EmailAddress).IsIn(query.RecipientEmailAddresses.ToArray())
                .UnderlyingCriteria;
        }
    }
}