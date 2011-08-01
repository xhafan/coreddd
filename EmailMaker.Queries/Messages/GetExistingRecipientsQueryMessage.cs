using System.Collections.Generic;
using Core.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetExistingRecipientsQueryMessage : IQueryMessage
    {
        public IEnumerable<string> RecipientEmailAddresses { get; set; }
    }
}