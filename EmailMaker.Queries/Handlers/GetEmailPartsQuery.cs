using CoreDdd.Queries;
using EmailMaker.Dtos.Emails;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailPartsQuery : BaseNhibernateCriteriaQueryHandler<Messages.GetEmailPartsQuery>
    {
        public override ICriteria GetCriteria<TResult>(Messages.GetEmailPartsQuery query)
        {
            return Session.QueryOver<EmailPartDto>()
                .Where(e => e.EmailId == query.EmailId)
                .UnderlyingCriteria;
        }
    }
}