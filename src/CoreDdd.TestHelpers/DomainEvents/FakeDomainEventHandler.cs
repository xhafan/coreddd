using System;
using CoreDdd.Domain.Events;

namespace CoreDdd.TestHelpers.DomainEvents
{
    internal class FakeDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly Action<TDomainEvent> _onDomainEventHandled;

        public FakeDomainEventHandler(Action<TDomainEvent> onDomainEventHandled)
        {
            _onDomainEventHandled = onDomainEventHandled;
        }

        public void Handle(TDomainEvent domainEvent)
        {
            _onDomainEventHandled(domainEvent);
        }
    }
}