using Core.Dtos;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDetailsDto : Dto
    {
        public virtual int EmailTemplateId { get; set; }
        public virtual string Name { get; set; }
        public virtual int UserId { get; set; }
    }
}