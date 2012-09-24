using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailTemplateQueryMessage : IQueryMessage
    {
        public int EmailTemplateId { get; set; }
    }
}
