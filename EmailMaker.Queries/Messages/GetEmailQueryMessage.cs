using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailQueryMessage : IQueryMessage
    {
        public int EmailId { get; set; }
    }
}