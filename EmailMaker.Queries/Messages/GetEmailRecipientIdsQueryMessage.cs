using Core.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailRecipientIdsQueryMessage : IQueryMessage
    {
        public int EmailId { get; set; }
    }
}