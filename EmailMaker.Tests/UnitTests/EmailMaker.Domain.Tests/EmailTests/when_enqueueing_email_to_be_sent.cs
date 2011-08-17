using System.Linq;
using Core.TestHelper.DomainEvents;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.Events.Emails;
using EmailMaker.TestHelper.Builders;
using Iesi.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTests
{
    [TestFixture]
    public class when_enqueueing_email_to_be_sent : BaseDomainEventTest<EmailEnqueuedToBeSentEvent>
    {
        private Email _email;
        private string _fromAddress = "from address";
        private string _toAddress1 = "to address1";
        private string _toAddress2 = "to address2";
        private string _subject = "subject";
        private int _emailId = 56;
        private string _name1 = "name1";
        private string _name2 = "name2";
        private Recipient _recipientOne;
        private Recipient _recipientTwo;

        [SetUp]
        public void Context()
        {            
            var template = EmailTemplateBuilder.New.Build();
            _email = EmailBuilder.New
                .WithEmailTemplate(template)
                .WithId(_emailId)
                .Build();
            _recipientOne = new Recipient(_toAddress1, _name1);
            _recipientTwo = new Recipient(_toAddress2, _name2);
            _email.EnqueueEmailToBeSent(_fromAddress,
                                        new HashedSet<Recipient>
                                            {
                                                _recipientOne,
                                                _recipientTwo
                                            },
                                        _subject);
        }

        [Test]
        public void email_state_changed_and_properties_set()
        {
            _email.State.ShouldBe(EmailState.ToBeSent);
            _email.FromAddress.ShouldBe(_fromAddress);
            _email.Subject.ShouldBe(_subject);
        }

        [Test]
        public void event_was_raised()
        {
            RaisedDomainEvent.EmailId.ShouldBe(_email.Id);
        }

        [Test]
        public void recipients_correctly_set()
        {
            _email.EmailRecipients.Count.ShouldBe(2);
            
            var emailRecipient = _email.EmailRecipients.First();
            emailRecipient.Recipient.ShouldBe(_recipientOne);
            emailRecipient.Sent.ShouldBe(false);
            emailRecipient.SentDate.ShouldBe(null);

            emailRecipient = _email.EmailRecipients.Last();
            emailRecipient.Recipient.ShouldBe(_recipientTwo);
            emailRecipient.Sent.ShouldBe(false);
            emailRecipient.SentDate.ShouldBe(null);
        }
    }
}