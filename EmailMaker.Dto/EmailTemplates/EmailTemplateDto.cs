using System.Collections.Generic;
using CoreDdd.Dtos;

namespace EmailMaker.Dtos.EmailTemplates
{
    public class EmailTemplateDto : Dto
    {
        public virtual int EmailTemplateId { get; set; }
        public virtual string Name { get; set; }
        public virtual IEnumerable<EmailTemplatePartDto> Parts { get; set; }
    }
}

