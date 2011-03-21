using System.Collections.Generic;

namespace EmailMaker.DTO.Emails
{
    public class EmailDTO
    {
        public virtual int EmailId { get; set; }
        public virtual IEnumerable<EmailPartDTO> Parts { get; set; }
    }
}