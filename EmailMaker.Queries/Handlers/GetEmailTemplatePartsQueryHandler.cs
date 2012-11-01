using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplatePartsQueryHandler : BaseQueryOverHandler<GetEmailTemplatePartsQuery>
    {
        public override IQueryOver GetCriteria<TResult>(GetEmailTemplatePartsQuery query)
        {
            return Session.QueryOver<EmailTemplatePartDto>()
                .Where(e => e.EmailTemplateId == query.EmailTemplateId);
        }
    }
}