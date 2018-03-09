using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailQuery : IQuery
    {
        public int EmailId { get; set; }
    }
}