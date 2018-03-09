using CoreTest;
using EmailMaker.Domain.Emails;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_creating_email_recipient : BaseTest
    {
        private Recipient _recipient;
        private EmailRecipient _emailRecipient;
        private Email _email;

        [SetUp]
        public void Context()
        {
            _recipient = A.Fake<Recipient>();
            _email = A.Fake<Email>();
            _emailRecipient = new EmailRecipient(_email, _recipient);
        }

        [Test]
        public void properties_are_set()
        {
            _emailRecipient.Email.ShouldBe(_email);
            _emailRecipient.Recipient.ShouldBe(_recipient);
        }
    }
}