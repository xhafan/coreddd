using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.QueryTests
{
    [TestFixture]
    public class when_querying_existing_recipients : BaseEmailMakerSimplePersistenceTest
    {
        private string _emailAddressOne = "email1@test.com";
        private string _emailAddressTwo = "email2@test.com";
        private string _emailAddressThree = "email3@test.com";
        private IEnumerable<Recipient> _result;
        private Recipient _recipientOne;
        private Recipient _recipientTwo;

        protected override void PersistenceContext()
        {
            _recipientOne = new Recipient(_emailAddressOne, "name1");
            Save(_recipientOne);

            _recipientTwo = new Recipient(_emailAddressTwo, "name2");
            Save(_recipientTwo);        
        }

        protected override void PersistenceQuery()
        {
            var query = new GetExistingRecipientsQuery();
            _result = query.Execute<Recipient>(new GetExistingRecipientsQueryMessage { RecipientEmailAddresses = new[] { _emailAddressOne, _emailAddressTwo, _emailAddressThree } });
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