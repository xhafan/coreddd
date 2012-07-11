using Core.Dtos;

namespace EmailMaker.DTO.EmailTemplates
{
    public class EmailTemplateDetailsDTO : Dto
    {
        public virtual int EmailTemplateId { get; set; }
        public virtual string Name { get; set; }
        public virtual int UserId { get; set; }
    }
}