using Core.Queries;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplateQuery : BaseNHibernateCriteriaQueryMessageHandler<GetEmailTemplateQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetEmailTemplateQueryMessage message)
        {
            return Session.QueryOver<EmailTemplate>()
                .Where(e => e.Id == message.TemplateId)
                //.Fetch(p => p.Parts).Eager
                .UnderlyingCriteria;
        }
    }
}