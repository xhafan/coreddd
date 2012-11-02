using System.Collections.Generic;
using CoreDdd.Dtos;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDto : Dto
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmailTemplatePartDto> Parts { get; set; }
    }
}

