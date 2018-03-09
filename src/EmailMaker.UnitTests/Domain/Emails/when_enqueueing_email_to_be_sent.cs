using System.Collections.Generic;
using System.Linq;
using CoreTest;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.Events.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_enqueueing_email_to_be_sent : BaseDomainEventTest<EmailEnqueuedToBeSentEvent>
    {
        private Email _email;
        private const string FromAddress = "from address";
        private const string ToAddressOne = "to address one";
        private const string ToAddressTwo = "to address two";
        private const string Subject = "subject";
        private const int EmailId = 56;
        private const string NameOne = "name one";
        private const string NameTwo = "name two";
        private Recipient _recipientOne;
        private Recipient _recipientTwo;

        [SetUp]
        public void Context()
        {            
            var template = EmailTemplateBuilder.New.Build();
            _email = new EmailBuilder()
                .WithEmailTemplate(template)
                .WithId(EmailId)
                .Build();
            _recipientOne = new Recipient(ToAddressOne, NameOne);
            _recipientTwo = new Recipient(ToAddressTwo, NameTwo);
            _email.EnqueueEmailToBeSent(FromAddress,
                                        new HashSet<Recipient>
                                            {
                                                _recipientOne,
                                                _recipientTwo
                                            },
                                        Subject);
        }

        [Test]
        public void email_state_changed_and_properties_set()
        {
            _email.State.ShouldBe(EmailState.ToBeSent);
            _email.FromAddress.ShouldBe(FromAddress);
            _email.Subject.ShouldBe(Subject);
        }

        [Test]
        public void event_was_raised()
        {
            RaisedDomainEvent.EmailId.ShouldBe(_email.Id);
        }

        [Test]
        public void recipients_correctly_set()
        {
            _email.EmailRecipients.Count().ShouldBe(2);
            
            var emailRecipient = _email.EmailRecipients.First();
            emailRecipient.Email.ShouldBe(_email);
            emailRecipient.Recipient.ShouldBe(_recipientOne);
            emailRecipient.Sent.ShouldBe(false);
            emailRecipient.SentDate.ShouldBe(null);

            emailRecipient = _email.EmailRecipients.Last();
            emailRecipient.Email.ShouldBe(_email);
            emailRecipient.Recipient.ShouldBe(_recipientTwo);
            emailRecipient.Sent.ShouldBe(false);
            emailRecipient.SentDate.ShouldBe(null);
        }
    }
}