using System.Collections.Generic;
using System.Text;
using CoreUtils.Extensions;
using EmailMaker.Core;
using EmailMaker.Domain.Emails;

namespace EmailMaker.Service.Handlers
{
    public class EmailHtmlBuilder : IEmailHtmlBuilder
    {
        public string BuildHtmlEmail(IEnumerable<EmailPart> emailParts)
        {
            var sb = new StringBuilder();
            emailParts.Each(part =>
            {
                switch (part)
                {
                    case HtmlEmailPart htmlEmailPart:
                        sb.Append(htmlEmailPart.Html);
                        break;
                    case VariableEmailPart variableEmailPart:
                        sb.Append(variableEmailPart.Value);
                        break;
                    default:
                        throw new EmailMakerException("Unknown part type: " + part);
                }
            });
            return sb.ToString();
        }
    }
}