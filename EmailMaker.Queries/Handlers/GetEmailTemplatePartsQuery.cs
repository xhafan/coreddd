using Core.Queries;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplatePartsQuery : BaseNHibernateCriteriaQueryMessageHandler<GetEmailTemplatePartsQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetEmailTemplatePartsQueryMessage message)
        {
            return Session.QueryOver<EmailTemplatePartDto>()
                .Where(e => e.EmailTemplateId == message.EmailTemplateId)
                .UnderlyingCriteria;
        }
    }
}