using CoreDdd.Queries;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplatePartsQuery : BaseNhibernateCriteriaQueryHandler<Messages.GetEmailTemplatePartsQuery>
    {
        public override ICriteria GetCriteria<TResult>(Messages.GetEmailTemplatePartsQuery query)
        {
            return Session.QueryOver<EmailTemplatePartDto>()
                .Where(e => e.EmailTemplateId == query.EmailTemplateId)
                .UnderlyingCriteria;
        }
    }
}