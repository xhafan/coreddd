using CoreDdd.Queries;
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailQueryHandler : BaseQueryOverHandler<GetEmailQuery>
    {
        public override IQueryOver GetQueryOver<TResult>(GetEmailQuery query)
        {
            return Session.QueryOver<EmailDto>()
                .Where(e => e.EmailId == query.EmailId);
        }
    }
}