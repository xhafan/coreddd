using Core.Queries;
using EmailMaker.DTO.EmailTemplates;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    // todo test is missing
    public class GetAllEmailTemplateQuery : BaseNHibernateCriteriaQueryMessageHandler<GetAllEmailTemplateQueryMessage>
    {
        public override ICriteria GetCriteria<TResult>(GetAllEmailTemplateQueryMessage message)
        {
            return Session.QueryOver<EmailTemplateDetailsDTO>()
                .UnderlyingCriteria;
        }
    }
}