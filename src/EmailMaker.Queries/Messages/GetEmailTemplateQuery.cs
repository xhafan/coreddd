using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailTemplateQuery : IQuery
    {
        public int EmailTemplateId { get; set; }
    }
}
