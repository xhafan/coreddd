using System.Net.Mail;
using EmailMaker.Messages;
using NServiceBus;

namespace EmailMaker.Service.Handlers
{
    public class SendEmailForEmailRecipientMessageHandler : IMessageHandler<SendEmailForEmailRecipientMessage>
    {
        public void Handle(SendEmailForEmailRecipientMessage message)
        {
            // todo: missing test
            var smtpClient = new SmtpClient("localhost");
            var fromMailAddress = new MailAddress(message.FromAddress);
            var toMailAddress = new MailAddress(message.RecipientEmailAddress, message.RecipientName);
            var mailMessage = new MailMessage(fromMailAddress, toMailAddress)
                                  {
                                      Subject = message.Subject,
                                      Body = message.EmailHtml,
                                      IsBodyHtml = true
                                  };
            smtpClient.Send(mailMessage);
        }
    }
}
