using System.Collections.Generic;
using CoreDto;

namespace EmailMaker.Dtos.Emails
{
    public class EmailDto : IDto
    {
        public int EmailId { get; set; }
        public IEnumerable<EmailPartDto> Parts { get; set; }
    }
}