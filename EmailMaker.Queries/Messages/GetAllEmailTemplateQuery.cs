using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetAllEmailTemplateQuery : IQuery
    {
        public int UserId { get; set; }
    }
}