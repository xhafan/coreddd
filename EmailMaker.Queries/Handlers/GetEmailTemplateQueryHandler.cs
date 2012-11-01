using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplateQueryHandler : BaseQueryOverHandler<GetEmailTemplateQuery>
    {
        public override IQueryOver GetCriteria<TResult>(GetEmailTemplateQuery query)
        {
            return Session.QueryOver<EmailTemplateDto>()
                .Where(e => e.EmailTemplateId == query.EmailTemplateId);
        }
    }
}