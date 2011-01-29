using Core.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailTemplateQueryMessage : IQueryMessage
    {
        public int TemplateId { get; set; }
    }
}
