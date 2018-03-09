using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailPartsQuery : IQuery
    {
        public int EmailId { get; set; }
    }
}