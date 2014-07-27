using System.Collections.Generic;

namespace EmailMaker.Dtos.Emails
{
    public class EmailDto
    {
        public int EmailId { get; set; }
        public IEnumerable<EmailPartDto> Parts { get; set; }
    }
}