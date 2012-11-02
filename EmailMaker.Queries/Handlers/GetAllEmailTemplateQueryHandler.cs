using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetAllEmailTemplateQueryHandler : BaseQueryOverHandler<Messages.GetAllEmailTemplateQuery>
    {
        public override IQueryOver GetQueryOver<TResult>(Messages.GetAllEmailTemplateQuery query)
        {
            return Session.QueryOver<EmailTemplateDetailsDto>()
                .Where(e => e.UserId == query.UserId);
        }
    }
}