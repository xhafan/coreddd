using System.Collections.Generic;
using CoreDdd.Dtos;

namespace EmailMaker.Dtos.Emails
{
    public class EmailDto : Dto
    {
        public int EmailId { get; set; }
        public IEnumerable<EmailPartDto> Parts { get; set; }
    }
}