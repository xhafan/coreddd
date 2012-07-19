using System.Collections.Generic;
using EmailMaker.Domain.Emails;

namespace EmailMaker.Service.Handlers
{
    public interface IEmailHtmlBuilder
    {
        string BuildHtmlEmail(IEnumerable<EmailPart> emailParts);
    }
}
