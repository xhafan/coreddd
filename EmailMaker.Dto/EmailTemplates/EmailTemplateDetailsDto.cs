using CoreDto;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDetailsDto : IDto
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}