using CoreDdd.Queries;
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailPartsQuery : BaseNHibernateCriteriaQueryMessageHandler<GetEmailPartsQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetEmailPartsQueryMessage message)
        {
            return Session.QueryOver<EmailPartDto>()
                .Where(e => e.EmailId == message.EmailId)
                .UnderlyingCriteria;
        }
    }
}