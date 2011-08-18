using System.Collections.Generic;
using Core.Domain;
using Core.Utilities.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Services;
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
            var messagesToBeSent = new List<SendEmailForEmailRecipientMessage>();
            email.EmailRecipients.Each(er => _bus.SendLocal(new SendEmailForEmailRecipientMessage
                                                                          {
                                                                              EmailId = email.Id,
                                                                              RecipientId = er.Recipient.Id,
                                                                              FromAddress = email.FromAddress,
                                                                              RecipientEmailAddress = er.Recipient.EmailAddress,
                                                                              RecipientName = er.Recipient.Name,
                                                                              Subject = email.Subject,
                                                                              EmailHtml = emailHtml
                                                                          }));
        }
    }
}
