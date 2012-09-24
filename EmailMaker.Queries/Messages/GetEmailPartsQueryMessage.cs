using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailPartsQueryMessage : IQueryMessage
    {
        public int EmailId { get; set; }
    }
}