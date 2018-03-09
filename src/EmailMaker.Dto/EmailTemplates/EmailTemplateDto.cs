using System.Collections.Generic;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDto
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmailTemplatePartDto> Parts { get; set; }
    }
}

