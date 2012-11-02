using CoreDdd.Queries;
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailPartsQueryHandler : BaseQueryOverHandler<GetEmailPartsQuery>
    {
        public override IQueryOver GetQueryOver<TResult>(Messages.GetEmailPartsQuery query)
        {
            return Session.QueryOver<EmailPartDto>()
                .Where(e => e.EmailId == query.EmailId);
        }
    }
}