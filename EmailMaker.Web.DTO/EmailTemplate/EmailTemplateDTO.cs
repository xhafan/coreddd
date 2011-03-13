using System.Collections.Generic;

namespace EmailMaker.DTO.EmailTemplate
{
    public class EmailTemplateDTO
    {
        public virtual int EmailTemplateId { get; set; }
        public virtual IEnumerable<EmailTemplatePartDTO> Parts { get; set; }
    }
}
