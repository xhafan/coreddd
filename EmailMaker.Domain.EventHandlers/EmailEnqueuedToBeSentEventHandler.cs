using Core.Domain.Events;
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
            throw new System.NotImplementedException();
            var message = new EmailEnqueuedToBeSentEventMessage();
            _bus.Send(message);
        }
    }
}
