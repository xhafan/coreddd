using System.Collections.Generic;
using CoreDdd.Nhibernate;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDto : IAutoMappedDto
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmailTemplatePartDto> Parts { get; set; }
    }
}

