using System.Collections.Generic;
using Core.Dtos;

namespace EmailMaker.DTO.Emails
{
    public class EmailDTO : Dto
    {
        public virtual int EmailId { get; set; }
        public virtual IEnumerable<EmailPartDTO> Parts { get; set; }
    }
}