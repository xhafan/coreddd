using CoreDdd.Domain.Events;
using EmailMaker.Domain.Events.Emails;
using EmailMaker.Messages;
using NServiceBus;

namespace EmailMaker.Domain.EventHandlers
{
    public class EmailEnqueuedToBeSentEventHandler : IDomainEventHandler<EmailEnqueuedToBeSentEvent>
    {
        private readonly IBus _bus;

        public EmailEnqueuedToBeSentEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(EmailEnqueuedToBeSentEvent domainEvent)
        {
            var message = new EmailEnqueuedToBeSentEventMessage { EmailId = domainEvent.EmailId };
            _bus.Send(message);
        }
    }
}
