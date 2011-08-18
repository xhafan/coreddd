using System.Collections.Generic;
using EmailMaker.Domain.Emails;

namespace EmailMaker.Domain.Services
{
    public class EmailHtmlBuilder : IEmailHtmlBuilder
    {
        public string BuildHtmlEmail(IEnumerable<EmailPart> emailParts)
        {
            //return "test";
            throw new System.NotImplementedException();
        }
    }
}