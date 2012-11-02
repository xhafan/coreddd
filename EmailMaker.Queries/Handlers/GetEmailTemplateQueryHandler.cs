using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplateQueryHandler : BaseQueryOverHandler<GetEmailTemplateQuery>
    {
        public override IQueryOver GetQueryOver<TResult>(GetEmailTemplateQuery query)
        {
            return Session.QueryOver<EmailTemplateDto>()
                .Where(e => e.EmailTemplateId == query.EmailTemplateId);
        }
    }
}