using System.Net.Mail;
using Castle.Core.Smtp;
using EmailMaker.Messages;
using NServiceBus;

namespace EmailMaker.Service.Handlers
{
    public class SendEmailForEmailRecipientMessageHandler : IMessageHandler<SendEmailForEmailRecipientMessage>
    {
        private readonly IEmailSender _emailSender;

        public SendEmailForEmailRecipientMessageHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Handle(SendEmailForEmailRecipientMessage message)
        {
            var fromMailAddress = new MailAddress(message.FromAddress);
            var toMailAddress = new MailAddress(message.RecipientEmailAddress, message.RecipientName);
            var mailMessage = new MailMessage(fromMailAddress, toMailAddress)
                                  {
                                      Subject = message.Subject,
                                      Body = message.EmailHtml,
                                      IsBodyHtml = true
                                  };
            _emailSender.Send(mailMessage);
        }
    }
}
