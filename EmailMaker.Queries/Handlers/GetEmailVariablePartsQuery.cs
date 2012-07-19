using Core.Queries;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailVariablePartsQuery : BaseNHibernateCriteriaQueryMessageHandler<GetEmailVariablePartsQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetEmailVariablePartsQueryMessage message)
        {
            return Session.QueryOver<EmailPartDto>()
                .Where(e => e.EmailId == message.EmailId && e.PartType == PartType.Variable)
                .UnderlyingCriteria;
        }
    }
}