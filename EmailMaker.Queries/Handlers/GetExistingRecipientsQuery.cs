using Core.Queries;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetExistingRecipientsQuery : BaseNHibernateCriteriaQueryMessageHandler<GetExistingRecipientsQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetExistingRecipientsQueryMessage message)
        {
            // todo: implemente XLOCK on the sql and write concurrent locking test for it
            throw new System.NotImplementedException(); 
        }
    }
}