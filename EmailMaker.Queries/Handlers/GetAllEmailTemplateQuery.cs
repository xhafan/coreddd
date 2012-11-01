using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetAllEmailTemplateQuery : BaseNhibernateCriteriaQueryHandler<Messages.GetAllEmailTemplateQuery>
    {
        public override ICriteria GetCriteria<TResult>(Messages.GetAllEmailTemplateQuery query)
        {
            return Session.QueryOver<EmailTemplateDetailsDto>()
                .Where(e => e.UserId == query.UserId)
                .UnderlyingCriteria;
        }
    }
}