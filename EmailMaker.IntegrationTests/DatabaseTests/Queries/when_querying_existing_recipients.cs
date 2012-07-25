using System.Collections.Generic;
using System.Linq;
using Core.Tests.Helpers.Persistence;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Queries
{
    [TestFixture]
    public class when_querying_existing_recipients : base_simple_persistence_test
    {
        private const string EmailAddressOne = "email1@test.com";
        private const string EmailAddressTwo = "email2@test.com";
        private const string EmailAddressThree = "email3@test.com";
        private IEnumerable<Recipient> _result;
        private Recipient _recipientOne;
        private Recipient _recipientTwo;

        protected override void PersistenceContext()
        {
            _recipientOne = new Recipient(EmailAddressOne, "name1");
            Save(_recipientOne);

            _recipientTwo = new Recipient(EmailAddressTwo, "name2");
            Save(_recipientTwo);        
        }

        protected override void PersistenceQuery()
        {
            var query = new GetExistingRecipientsQuery();
            _result = query.Execute<Recipient>(new GetExistingRecipientsQueryMessage { RecipientEmailAddresses = new[] { EmailAddressOne, EmailAddressTwo, EmailAddressThree } });
        }

        [Test]
        public void recipients_correctly_retrieved()
        {
            _result.Count().ShouldBe(2);
            _result.First().ShouldBe(_recipientOne);
            _result.Last().ShouldBe(_recipientTwo);
        }
    }
}