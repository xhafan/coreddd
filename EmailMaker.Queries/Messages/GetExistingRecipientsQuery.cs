using System.Collections.Generic;
using CoreDdd.Queries;

namespace EmailMaker.Queries.Messages
{
    public class GetExistingRecipientsQuery : IQuery
    {
        public IEnumerable<string> RecipientEmailAddresses { get; set; }
    }
}