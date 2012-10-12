using System;
using System.Collections.Generic;
using CoreTest;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Users;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public class EmailMakerAggregateRootTypesToClearProvider : IAggregateRootTypesToClearProvider
    {
        public IEnumerable<Type> GetAggregateRootTypesToClear()
        {
            yield return typeof(Email);
            yield return typeof(EmailTemplate);
            yield return typeof(Recipient);
            yield return typeof(User);
        }
    }
}