using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetUserDetailsByEmailAddressQuery : IQuery
    {
        public string EmailAddress { get; set; }
    }
}