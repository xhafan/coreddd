using Core.Queries;
using EmailMaker.DTO.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplatePartsQuery : BaseNHibernateCriteriaQueryMessageHandler<GetEmailTemplatePartsQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetEmailTemplatePartsQueryMessage message)
        {
            return Session.QueryOver<EmailTemplatePartDTO>()
                .Where(e => e.EmailTemplateId == message.EmailTemplateId)
                .UnderlyingCriteria;
        }
    }
}