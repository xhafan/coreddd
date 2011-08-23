using Core.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetUserDetailsByEmailAddressMessage : IQueryMessage
    {
        public string EmailAddress { get; set; }
    }
}