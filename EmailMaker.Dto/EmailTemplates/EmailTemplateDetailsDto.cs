using CoreDdd.Nhibernate;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDetailsDto : IAutoMappedDto
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}