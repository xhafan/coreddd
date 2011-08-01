using System.Collections.Generic;

namespace EmailMaker.Commands.Handlers
{
    public interface IRecipientParser
    {
        IDictionary<string, string> Parse(string recipientStr);
    }
}