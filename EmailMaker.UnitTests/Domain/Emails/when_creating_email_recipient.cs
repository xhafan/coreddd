using EmailMaker.Domain.Emails;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_creating_email_recipient
    {
        private Recipient _recipient;
        private EmailRecipient _emailRecipient;

        [SetUp]
        public void Context()
        {
            _recipient = new Recipient("email address", "name");
            _emailRecipient = new EmailRecipient(_recipient);
        }

        [Test]
        public void recipient_was_set()
        {
            _emailRecipient.Recipient.ShouldBe(_recipient);
        }
    }
}