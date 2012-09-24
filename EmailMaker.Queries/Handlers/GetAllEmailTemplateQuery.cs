using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetAllEmailTemplateQuery : BaseNHibernateCriteriaQueryMessageHandler<GetAllEmailTemplateQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetAllEmailTemplateQueryMessage message)
        {
            return Session.QueryOver<EmailTemplateDetailsDto>()
                .Where(e => e.UserId == message.UserId)
                .UnderlyingCriteria;
        }
    }
}