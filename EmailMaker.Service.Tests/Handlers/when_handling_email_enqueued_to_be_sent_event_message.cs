using System.Collections.Generic;
using System.Linq;
using CoreDdd.Domain.Repositories;
using CoreNserviceBusTest.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Messages;
using EmailMaker.Service.Handlers;
using EmailMaker.TestHelper.Builders;
using FakeItEasy;
using NServiceBus;
using NUnit.Framework;
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
        private readonly List<SendEmailForEmailRecipientMessage> _sentMessages = new List<SendEmailForEmailRecipientMessage>();

        [SetUp]
        public void Context()
        {
            _email = A.Fake<Email>();
            var emailParts = new EmailPart[0];
            A.CallTo(() => _email.Id).Returns(EmailId);
            A.CallTo(() => _email.Parts).Returns(emailParts);

            _recipientOne = RecipientBuilder.New.WithId(RecipientOneId).WithEmailAddress(RecipientOneEmailAddress).WithName(RecipientOneName).Build();
            _recipientTwo = RecipientBuilder.New.WithId(RecipientTwoId).WithEmailAddress(RecipientTwoEmailAddress).WithName(RecipientTwoName).Build();
            A.CallTo(() => _email.EmailRecipients).Returns(new HashSet<EmailRecipient>
                                                           {
                                                               new EmailRecipient(_email, _recipientOne),
                                                               new EmailRecipient(_email, _recipientTwo)
                                                           });

            A.CallTo(() => _email.FromAddress).Returns(FromAddress);
            A.CallTo(() => _email.Subject).Returns(Subject);

            var emailRepository = A.Fake<IRepository<Email>>();
            A.CallTo(() => emailRepository.GetById(EmailId)).Returns(_email);

            var emailHtmlBuilder = A.Fake<IEmailHtmlBuilder>();
            A.CallTo(() => emailHtmlBuilder.BuildHtmlEmail(emailParts)).Returns(EmailHtml);

            _bus = A.Fake<IBus>();
            _bus.ExpectMessagesSentLocally<SendEmailForEmailRecipientMessage>(x => _sentMessages.AddRange(x));

            var handler = new EmailEnqueuedToBeSentEventMessageHandler(emailRepository, emailHtmlBuilder, _bus);
            handler.Handle(new EmailEnqueuedToBeSentEventMessage {EmailId = EmailId});

        }

        [Test]
        public void messages_were_sent_for_each_email_recipient()
        {
            _sentMessages.Count.ShouldBe(2);

            var sentMessage = _sentMessages.First();
            sentMessage.EmailId.ShouldBe(EmailId);
            sentMessage.RecipientId.ShouldBe(RecipientOneId);
            sentMessage.FromAddress.ShouldBe(FromAddress);
            sentMessage.RecipientEmailAddress.ShouldBe(RecipientOneEmailAddress);
            sentMessage.RecipientName.ShouldBe(RecipientOneName);
            sentMessage.Subject.ShouldBe(Subject);
            sentMessage.EmailHtml.ShouldBe(EmailHtml);

            sentMessage = _sentMessages.Last();
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
