using System.Collections.Generic;

namespace EmailMaker.DTO.EmailTemplates
{
    public class EmailTemplateDTO
    {
        public virtual int EmailTemplateId { get; set; }
        public virtual string Name { get; set; }
        public virtual IEnumerable<EmailTemplatePartDTO> Parts { get; set; }
    }
}

