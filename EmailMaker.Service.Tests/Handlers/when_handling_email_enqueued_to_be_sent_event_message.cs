using System.Collections.Generic;
using System.Linq;
using CoreDdd.Domain.Repositories;
using CoreNserviceBusTest.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Messages;
using EmailMaker.Service.Handlers;
using EmailMaker.TestHelper.Builders;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace EmailMaker.Service.Tests.Handlers
{
    [TestFixture]
    public class when_handling_email_enqueued_to_be_sent_event_message
    {
        private const int EmailId = 23;
        private IBus _bus;
        private const string EmailHtml = "email html";
        private const string FromAddress = "from address";
        private const string Subject = "subject";
        private Recipient _recipientOne;
        private Recipient _recipientTwo;
        private const int RecipientOneId = 56;
        private const int RecipientTwoId = 57;
        private const string RecipientOneEmailAddress = "email one";
        private const string RecipientTwoEmailAddress = "email two";
        private const string RecipientOneName = "name one";
        private const string RecipientTwoName = "name two";
        private Email _email;

        [SetUp]
        public void Context()
        {
            _email = MockRepository.GenerateMock<Email>();
            var emailParts = new EmailPart[0];
            _email.Stub(a => a.Id).Return(EmailId);
            _email.Stub(a => a.Parts).Return(emailParts);

            _recipientOne = RecipientBuilder.New.WithId(RecipientOneId).WithEmailAddress(RecipientOneEmailAddress).WithName(RecipientOneName).Build();
            _recipientTwo = RecipientBuilder.New.WithId(RecipientTwoId).WithEmailAddress(RecipientTwoEmailAddress).WithName(RecipientTwoName).Build();
            _email.Stub(a => a.EmailRecipients).Return(new HashSet<EmailRecipient>
                                                          {
                                                              new EmailRecipient(_email, _recipientOne),
                                                              new EmailRecipient(_email, _recipientTwo)
                                                          });

            _email.Stub(a => a.FromAddress).Return(FromAddress);
            _email.Stub(a => a.Subject).Return(Subject);

            var emailRepository = MockRepository.GenerateStub<IRepository<Email>>();
            emailRepository.Stub(a => a.GetById(EmailId)).Return(_email);

            var emailHtmlBuilder = MockRepository.GenerateStub<IEmailHtmlBuilder>();
            emailHtmlBuilder.Stub(a => a.BuildHtmlEmail(emailParts)).Return(EmailHtml);

            _bus = MockRepository.GenerateStub<IBus>();

            var handler = new EmailEnqueuedToBeSentEventMessageHandler(emailRepository, emailHtmlBuilder, _bus);
            handler.Handle(new EmailEnqueuedToBeSentEventMessage {EmailId = EmailId});

        }

        [Test]
        public void messages_were_sent_for_each_email_recipient()
        {
            var sentMessages = _bus.MessagesShouldHaveBeenSentLocally<SendEmailForEmailRecipientMessage>().ToList();
            
            var sentMessage = sentMessages.First();
            sentMessage.EmailId.ShouldBe(EmailId);
            sentMessage.RecipientId.ShouldBe(RecipientOneId);
            sentMessage.FromAddress.ShouldBe(FromAddress);
            sentMessage.RecipientEmailAddress.ShouldBe(RecipientOneEmailAddress);
            sentMessage.RecipientName.ShouldBe(RecipientOneName);
            sentMessage.Subject.ShouldBe(Subject);
            sentMessage.EmailHtml.ShouldBe(EmailHtml);

            sentMessage = sentMessages.Last();
            sentMessage.EmailId.ShouldBe(EmailId);
            sentMessage.RecipientId.ShouldBe(RecipientTwoId);
            sentMessage.FromAddress.ShouldBe(FromAddress);
            sentMessage.RecipientEmailAddress.ShouldBe(RecipientTwoEmailAddress);
            sentMessage.RecipientName.ShouldBe(RecipientTwoName);
            sentMessage.Subject.ShouldBe(Subject);
            sentMessage.EmailHtml.ShouldBe(EmailHtml);        
        }
    }
}
