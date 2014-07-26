using System.Linq;
using CoreDdd.TestHelpers.Extensions;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.TestHelper.Builders;
using EmailMaker.TestHelper.Extensions;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Domain.Emails
{
    [TestFixture]
    public class when_persisting_email_with_recipients : BaseEmailMakerSimplePersistenceTest
    {
        private Email _email;
        private Email _retrievedEmail;
        private EmailTemplate _emailTemplate;
        private const string EmailOne = "emailOne@test.com";
        private const string EmailTwo = "emailtwo@test.com";
        private Recipient _recipientOne;
        private Recipient _recipientTwo;
        private const string FromAddress = "fromAddress@test.com";
        private const string Subject = "subject";

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            _emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("123")
                .WithName("template name")
                .WithUserId(user.Id)
                .Build();
            Save(_emailTemplate);

            _recipientOne = new Recipient(EmailOne, "name one");
            _recipientTwo = new Recipient(EmailTwo, "name two");
            Save(_recipientOne);
            Save(_recipientTwo);

            _email = new Email(_emailTemplate);
            Save(_email);

            _email.SetPrivateProperty("FromAddress", FromAddress);
            _email.EmailRecipients.AsSet().Add(new EmailRecipient(_email, _recipientOne));
            _email.EmailRecipients.AsSet().Add(new EmailRecipient(_email, _recipientTwo));
            
            _email.SetPrivateProperty("Subject", Subject);

            Save(_email);
        }

        protected override void PersistenceQuery()
        {
            _retrievedEmail = Get<Email>(_email.Id);
        }

        [Test]
        public void email_details_correctly_retrieved()
        {
            _retrievedEmail.State.ShouldBe(EmailState.Draft);
            _retrievedEmail.FromAddress.ShouldBe(FromAddress);
            _retrievedEmail.Subject.ShouldBe(Subject);
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