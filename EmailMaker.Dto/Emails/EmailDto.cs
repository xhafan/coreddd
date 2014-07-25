using System.Collections.Generic;
using CoreDdd.Nhibernate;

namespace EmailMaker.Dtos.Emails
{
    public class EmailDto : IAutoMappedDto
    {
        public int EmailId { get; set; }
        public IEnumerable<EmailPartDto> Parts { get; set; }
    }
}