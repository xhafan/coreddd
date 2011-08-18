using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.DTO.Emails;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.QueryTests
{
//    [TestFixture]
//    public class when_querying_email_recipients : BaseSimplePersistenceTest
//    {
//        private IEnumerable<EmailRecipientDTO> _result;
//        private Email _email;
//        private EmailRecipient _emailRecipientOne;
//        private EmailRecipient _emailRecipientTwo;
//
//        public override void PersistenceContext()
//        {
//            var emailTemplate = new EmailTemplate("123");
//            Save(emailTemplate);
//
//            var recipientOne = new Recipient("email one", "name one");
//            var recipientTwo = new Recipient("email two", "name two");
//
//            _email = new Email(emailTemplate);
//            _emailRecipientOne = new EmailRecipient(recipientOne);
//            _email.EmailRecipients.Add(_emailRecipientOne);
//            _emailRecipientTwo = new EmailRecipient(recipientTwo);
//            _email.EmailRecipients.Add(_emailRecipientTwo);
//
//            var anotherEmail = new Email(emailTemplate);
//            anotherEmail.EmailRecipients.Add(new EmailRecipient(recipientOne));
//            Save(_email, anotherEmail);
//        }
//
//        public override void PersistenceQuery()
//        {
//            var query = new GetEmailRecipientIdsQuery();
//            _result = query.Execute<EmailRecipientDTO>(new GetEmailRecipientIdsQueryMessage {EmailId = _email.Id});
//        }
//
//        [Test]
//        public void email_recipients_correctly_retrieved()
//        {
//            _result.Count().ShouldBe(2);
//
//            var emailRecipient = _result.First();
//            emailRecipient.EmailId.ShouldBe(_email.Id);
//            emailRecipient.EmailRecipientId.ShouldBe(_emailRecipientOne.Id);
//
//            emailRecipient = _result.Last();
//            emailRecipient.EmailId.ShouldBe(_email.Id);
//            emailRecipient.EmailRecipientId.ShouldBe(_emailRecipientTwo.Id);
//        }
//    }
}