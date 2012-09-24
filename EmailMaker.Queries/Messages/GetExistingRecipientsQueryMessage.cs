using System.Collections.Generic;
using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetExistingRecipientsQueryMessage : IQueryMessage
    {
        public IEnumerable<string> RecipientEmailAddresses { get; set; }
    }
}