using System.Globalization;

namespace EmailMaker.DTO.EmailTemplates
{
    public class EmailTemplateDetailsDTO
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual CultureInfo Culture { get; set; }
        public virtual int EmailTemplateId { get; set; }
    }
}