using CoreDdd.Dtos;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDetailsDto : Dto
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}