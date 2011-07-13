using System.Collections.Generic;
using Core.TestHelper.DomainEvents;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.Events.Emails;
using EmailMaker.TestHelper.Builders;
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

        [SetUp]
        public void Context()
        {            
            var template = EmailTemplateBuilder.New.Build();
            _email = EmailBuilder.New
                .WithEmailTemplate(template)
                .WithId(_emailId)
                .WithRecipient("toAddressThree", "name3")
                .WithRecipient(_toAddress1, _name1)
                .Build();
            _email.EnqueueEmailToBeSent(_fromAddress,
                                        new Dictionary<string, Recipient>
                                            {
                                                {_toAddress1, new Recipient(_toAddress1, _name1)},
                                                {_toAddress2, new Recipient(_toAddress2, _name2)}
                                            },
                                        _subject);
        }

        [Test]
        public void email_state_changed_and_properties_set()
        {
            _email.State.ShouldBe(EmailState.ToBeSent);
            _email.FromAddress.ShouldBe(_fromAddress);
            _email.Subject.ShouldBe(_subject);
            _email.Recipients[_toAddress1].EmailAddress.ShouldBe(_toAddress1);
            _email.Recipients[_toAddress1].Name.ShouldBe(_name1);
            _email.Recipients[_toAddress2].EmailAddress.ShouldBe(_toAddress2);
            _email.Recipients[_toAddress2].Name.ShouldBe(_name2);
        }

        [Test]
        public void event_was_raised()
        {
            RaisedDomainEvent.EmailId.ShouldBe(_email.Id);
        }

        [Test]
        public void recipients_correctly_set()
        {
            _email.Recipients.Count.ShouldBe(2);

            var recipient = _email.Recipients[_toAddress1];
            recipient.EmailAddress.ShouldBe(_toAddress1);
            recipient.Name.ShouldBe(_name1);

            recipient = _email.Recipients[_toAddress2];
            recipient.EmailAddress.ShouldBe(_toAddress2);
            recipient.Name.ShouldBe(_name2);
        }
    }
}