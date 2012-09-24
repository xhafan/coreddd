using System.Collections.Generic;
using CoreDdd.Dtos;

namespace EmailMaker.Dtos.Emails
{
    public class EmailDto : Dto
    {
        public virtual int EmailId { get; set; }
        public virtual IEnumerable<EmailPartDto> Parts { get; set; }
    }
}