using CoreDdd.Queries;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailVariablePartsQuery : BaseNhibernateCriteriaQueryHandler<Messages.GetEmailVariablePartsQuery>
    {
        public override ICriteria GetCriteria<TResult>(Messages.GetEmailVariablePartsQuery query)
        {
            return Session.QueryOver<EmailPartDto>()
                .Where(e => e.EmailId == query.EmailId && e.PartType == PartType.Variable)
                .UnderlyingCriteria;
        }
    }
}