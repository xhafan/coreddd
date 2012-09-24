using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplateQuery : BaseNHibernateCriteriaQueryMessageHandler<GetEmailTemplateQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetEmailTemplateQueryMessage message)
        {
            return Session.QueryOver<EmailTemplateDto>()
                .Where(e => e.EmailTemplateId == message.EmailTemplateId)
                .UnderlyingCriteria;
        }
    }
}