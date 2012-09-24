using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailTemplatePartsQueryMessage : IQueryMessage
    {
        public int EmailTemplateId { get; set; }
    }
}