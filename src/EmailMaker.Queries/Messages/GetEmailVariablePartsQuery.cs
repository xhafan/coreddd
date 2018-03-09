using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailVariablePartsQuery : IQuery
    {
        public int EmailId { get; set; }
    }
}