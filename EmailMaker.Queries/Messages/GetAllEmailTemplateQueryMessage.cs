using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetAllEmailTemplateQueryMessage : IQueryMessage
    {
        public int UserId { get; set; }
    }
}