using System.Collections.Generic;
using CoreDto;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDto : IDto
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmailTemplatePartDto> Parts { get; set; }
    }
}

