using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Extensions;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using Iesi.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.EmailPersistenceTests
{
    [TestFixture]
    public class when_persisting_email_with_recipients : BaseSimplePersistenceTest
    {
        private Email _email;
        private Email _retrievedEmail;
        private EmailTemplate _emailTemplate;
        private string _emailOne = "emailOne@test.com";
        private string _emailTwo = "emailtwo@test.com";
        private Recipient _recipientOne;
        private Recipient _recipientTwo;
        private string _fromAddress = "fromAddress@test.com";
        private string _subject = "subject";

        public override void PersistenceContext()
        {
            _emailTemplate = new EmailTemplate("123");
            Save(_emailTemplate);

            _recipientOne = new Recipient(_emailOne, "name one");
            _recipientTwo = new Recipient(_emailTwo, "name two");
            Save(_recipientOne);
            Save(_recipientTwo);

            _email = new Email(_emailTemplate);
            Save(_email);

            _email.SetPrivateProperty("FromAddress", _fromAddress);
            _email.EmailRecipients.Add(new EmailRecipient(_recipientOne));
            _email.EmailRecipients.Add(new EmailRecipient(_recipientTwo));
            
            _email.SetPrivateProperty("Subject", _subject);

            Save(_email);
        }

        public override void PersistenceQuery()
        {
            _retrievedEmail = Get<Email>(_email.Id);
        }

        [Test]
        public void email_details_correctly_retrieved()
        {
            _retrievedEmail.State.ShouldBe(EmailState.Draft);
            _retrievedEmail.FromAddress.ShouldBe(_fromAddress);
            _retrievedEmail.Subject.ShouldBe(_subject);
        }

        [Test]
        public void email_recipients_correctly_retrieved()
        {
            _retrievedEmail.EmailRecipients.Count().ShouldBe(2);

            var emailRecipient = _retrievedEmail.EmailRecipients.First();
            emailRecipient.Recipient.ShouldBe(_recipientOne);
            emailRecipient.Sent.ShouldBe(false);
            emailRecipient.SentDate.ShouldBe(null);

            emailRecipient = _retrievedEmail.EmailRecipients.Last();
            emailRecipient.Recipient.ShouldBe(_recipientTwo);
            emailRecipient.Sent.ShouldBe(false);
            emailRecipient.SentDate.ShouldBe(null);
        }
    }
}