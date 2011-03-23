using Core.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailVariablePartsQueryMessage : IQueryMessage
    {
        public int EmailId { get; set; }
    }
}