using System.Collections.Generic;
using EmailMaker.Domain.Emails;

namespace EmailMaker.Domain.Services
{
    public interface IEmailHtmlBuilder
    {
        string BuildHtmlEmail(IEnumerable<EmailPart> emailParts);
    }
}
