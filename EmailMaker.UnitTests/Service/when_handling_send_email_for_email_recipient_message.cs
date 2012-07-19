using System.Linq;
using System.Net.Mail;
using Castle.Core.Smtp;
using EmailMaker.Messages;
using EmailMaker.Service.Handlers;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.UnitTests.Service
{
    [TestFixture]
    public class when_handling_send_email_for_email_recipient_message
    {
        private int _emailId = 23;
        private string _emailHtml = "email html";
        private string _fromAddress = "\"John Smith\" <fromAddress@test.com>";
        private string _subject = "subject";
        private int _recipientId = 56;
        private string _recipientEmailAddress = "recipient@test.com";
        private string _recipientName = "name one";
        private IEmailSender _emailSender;

        [SetUp]
        public void Context()
        {
            _emailSender = MockRepository.GenerateMock<IEmailSender>();
            var handler = new SendEmailForEmailRecipientMessageHandler(_emailSender);
            handler.Handle(new SendEmailForEmailRecipientMessage
                               {
                                   EmailId = _emailId,
                                   RecipientId = _recipientId,
                                   EmailHtml = _emailHtml,
                                   FromAddress = _fromAddress,
                                   RecipientEmailAddress = _recipientEmailAddress,
                                   RecipientName = _recipientName,
                                   Subject = _subject
                               });

        }

        [Test]
        public void email_was_sent()
        {
            _emailSender.AssertWasCalled(a => a.Send(Arg<MailMessage>.Matches(p => 
                _MatchMailMessage(p)
                )));        
        }

        private bool _MatchMailMessage(MailMessage p)
        {
            return p.From.ToString() == _fromAddress
                   && p.To.First().ToString() == "\"" + _recipientName + "\" <" + _recipientEmailAddress + ">"
                   && p.Subject == _subject
                   && p.IsBodyHtml
                   && p.Body == _emailHtml;
        }
    }
}