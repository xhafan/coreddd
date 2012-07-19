using System.Linq;
using Core.Domain;
using Core.Tests.Helpers.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Messages;
using EmailMaker.Service.Handlers;
using EmailMaker.TestHelper.Builders;
using Iesi.Collections.Generic;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace EmailMaker.UnitTests.Service
{
    [TestFixture]
    public class when_handling_email_enqueued_to_be_sent_event_message
    {
        private int _emailId = 23;
        private IBus _bus;
        private string _emailHtml = "email html";
        private string _fromAddress = "from address";
        private string _subject = "subject";
        private Recipient _recipientOne;
        private Recipient _recipientTwo;
        private int _recipientOneId = 56;
        private int _recipientTwoId = 57;
        private string _recipientOneEmailAddress = "email one";
        private string _recipientTwoEmailAddress = "email two";
        private string _recipientOneName = "name one";
        private string _recipientTwoName = "name two";
        private Email _email;

        [SetUp]
        public void Context()
        {
            _email = MockRepository.GenerateMock<Email>();
            var emailParts = new EmailPart[0];
            _email.Stub(a => a.Id).Return(_emailId);
            _email.Stub(a => a.Parts).Return(emailParts);

            _recipientOne = RecipientBuilder.New.WithId(_recipientOneId).WithEmailAddress(_recipientOneEmailAddress).WithName(_recipientOneName).Build();
            _recipientTwo = RecipientBuilder.New.WithId(_recipientTwoId).WithEmailAddress(_recipientTwoEmailAddress).WithName(_recipientTwoName).Build();
            _email.Stub(a => a.EmailRecipients).Return(new HashedSet<EmailRecipient>
                                                          {
                                                              new EmailRecipient(_recipientOne),
                                                              new EmailRecipient(_recipientTwo)
                                                          });

            _email.Stub(a => a.FromAddress).Return(_fromAddress);
            _email.Stub(a => a.Subject).Return(_subject);

            var emailRepository = MockRepository.GenerateStub<IRepository<Email>>();
            emailRepository.Stub(a => a.GetById(_emailId)).Return(_email);

            var emailHtmlBuilder = MockRepository.GenerateStub<IEmailHtmlBuilder>();
            emailHtmlBuilder.Stub(a => a.BuildHtmlEmail(emailParts)).Return(_emailHtml);

            _bus = MockRepository.GenerateStub<IBus>();

            var handler = new EmailEnqueuedToBeSentEventMessageHandler(emailRepository, emailHtmlBuilder, _bus);
            handler.Handle(new EmailEnqueuedToBeSentEventMessage {EmailId = _emailId});

        }

        [Test]
        public void messages_were_sent_for_each_email_recipient()
        {
            var sentMessages = _bus.MessagesShouldHaveBeenSentLocally<SendEmailForEmailRecipientMessage>();
            
            var sentMessage = sentMessages.First();
            sentMessage.EmailId.ShouldBe(_emailId);
            sentMessage.RecipientId.ShouldBe(_recipientOneId);
            sentMessage.FromAddress.ShouldBe(_fromAddress);
            sentMessage.RecipientEmailAddress.ShouldBe(_recipientOneEmailAddress);
            sentMessage.RecipientName.ShouldBe(_recipientOneName);
            sentMessage.Subject.ShouldBe(_subject);
            sentMessage.EmailHtml.ShouldBe(_emailHtml);

            sentMessage = sentMessages.Last();
            sentMessage.EmailId.ShouldBe(_emailId);
            sentMessage.RecipientId.ShouldBe(_recipientTwoId);
            sentMessage.FromAddress.ShouldBe(_fromAddress);
            sentMessage.RecipientEmailAddress.ShouldBe(_recipientTwoEmailAddress);
            sentMessage.RecipientName.ShouldBe(_recipientTwoName);
            sentMessage.Subject.ShouldBe(_subject);
            sentMessage.EmailHtml.ShouldBe(_emailHtml);        
        }
    }
}
