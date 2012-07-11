using System.Collections.Generic;
using Core.Dtos;

namespace EmailMaker.DTO.EmailTemplates
{
    public class EmailTemplateDTO : Dto
    {
        public virtual int EmailTemplateId { get; set; }
        public virtual string Name { get; set; }
        public virtual IEnumerable<EmailTemplatePartDTO> Parts { get; set; }
    }
}

