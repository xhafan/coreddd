using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace EmailMaker.Commands.Handlers
{
    public class RecipientParser : IRecipientParser
    {
        private static readonly string[] Delimiters = { ";", ",", "\r\n", "\n" };

        public IDictionary<string, string> Parse(string recipientStr)
        {
            return recipientStr.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Where(emailAndName => !string.IsNullOrWhiteSpace(emailAndName))
                .Select(emailAndName => new MailAddress(emailAndName.Trim()))
                .ToDictionary(k => k.Address, v => v.DisplayName);
        }
    }
}
