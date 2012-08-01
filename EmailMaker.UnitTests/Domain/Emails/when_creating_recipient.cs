using EmailMaker.Domain.Emails;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_creating_recipient
    {
        private const string EmailAddress = "email address";
        private const string Name = "name";
        private Recipient _recipient;

        [SetUp]
        public void Context()
        {
            _recipient = new Recipient(EmailAddress, Name);
        }

        [Test]
        public void properties_were_set()
        {
            _recipient.EmailAddress.ShouldBe(EmailAddress);
            _recipient.Name.ShouldBe(Name);
        }
    }
}