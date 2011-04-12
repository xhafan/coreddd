using System.Linq;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTests
{
    [TestFixture]
    public class when_enqueueing_email_to_be_sent
    {
        private Email _email;
        private string _fromAddress = "from address";
        private string _toAddress1 = "to address1";
        private string _toAddress2 = "to address2";
        private string _subject = "subject";

        [SetUp]
        public void Context()
        {
            var template = EmailTemplateBuilder.New.Build();
            _email = EmailBuilder.New
                .WithEmailTemplate(template)
                .Build();
            _email.EnqueueEmailToBeSent(_fromAddress, new[]{ _toAddress1, _toAddress2}, _subject);
        }

        [Test]
        public void email_state_changed_and_properties_set()
        {
            _email.State.ShouldBe(EmailState.ToBeSent);
            _email.FromAddress.ShouldBe(_fromAddress);
            _email.ToAddresses.ShouldContain(_toAddress1);
            _email.ToAddresses.ShouldContain(_toAddress2);
            _email.Subject.ShouldBe(_subject);            
        }
    }
}