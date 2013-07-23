using CoreDdd.Domain.Repositories;
using CoreUtils.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Messages;
using NServiceBus;

namespace EmailMaker.Service.Handlers
{
    public class EmailEnqueuedToBeSentEventMessageHandler : IMessageHandler<EmailEnqueuedToBeSentEventMessage>
    {
        private readonly IEmailHtmlBuilder _emailHtmlBuilder;
        private readonly IBus _bus;
        private readonly IRepository<Email> _emailRepository;

        public EmailEnqueuedToBeSentEventMessageHandler(IRepository<Email> emailRepository, IEmailHtmlBuilder emailHtmlBuilder, IBus bus)
        {
            _emailRepository = emailRepository;
            _bus = bus;
            _emailHtmlBuilder = emailHtmlBuilder;
        }

        public void Handle(EmailEnqueuedToBeSentEventMessage message)
        {
            var email = _emailRepository.GetById(message.EmailId);
            var emailHtml = _emailHtmlBuilder.BuildHtmlEmail(email.Parts);
            email.EmailRecipients.Each(x => _bus.SendLocal(new SendEmailForEmailRecipientMessage
                                                                          {
                                                                              EmailId = email.Id,
                                                                              RecipientId = x.Recipient.Id,
                                                                              FromAddress = email.FromAddress,
                                                                              RecipientEmailAddress = x.Recipient.EmailAddress,
                                                                              RecipientName = x.Recipient.Name,
                                                                              Subject = email.Subject,
                                                                              EmailHtml = emailHtml
                                                                          }));
        }
    }
}
