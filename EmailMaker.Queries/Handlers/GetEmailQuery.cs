using Core.Queries;
using EmailMaker.DTO.Emails;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailQuery : BaseNHibernateCriteriaQueryMessageHandler<GetEmailQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetEmailQueryMessage message)
        {
            return Session.QueryOver<EmailDTO>()
                .Where(e => e.EmailId == message.EmailId)
                .UnderlyingCriteria;
        }
    }
}