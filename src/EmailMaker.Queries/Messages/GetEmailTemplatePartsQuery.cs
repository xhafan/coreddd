using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetEmailTemplatePartsQuery : IQuery
    {
        public int EmailTemplateId { get; set; }
    }
}