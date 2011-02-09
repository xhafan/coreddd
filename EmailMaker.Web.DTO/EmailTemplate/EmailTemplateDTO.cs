using System.Collections.Generic;

namespace EmailMaker.Web.DTO.EmailTemplate
{
    public class EmailTemplateDTO
    {
        public int EmailTemplateId { get; set; }
        public IEnumerable<EmailPartDTO> Parts { get; set; }

    }
}
