using Core.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetAllEmailTemplateQueryMessage : IQueryMessage
    {
        public int UserId { get; set; }
    }
}