using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplateQuery : BaseNhibernateCriteriaQueryHandler<Messages.GetEmailTemplateQuery>
    {
        public override ICriteria GetCriteria<TResult>(Messages.GetEmailTemplateQuery query)
        {
            return Session.QueryOver<EmailTemplateDto>()
                .Where(e => e.EmailTemplateId == query.EmailTemplateId)
                .UnderlyingCriteria;
        }
    }
}