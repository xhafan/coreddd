using System;
using System.Collections.Generic;
using CoreDdd.Domain.Events;

namespace CoreDdd.TestHelpers.DomainEvents
{
    public class FakeDomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        private readonly Action<object> _onDomainEventHandled;

        public FakeDomainEventHandlerFactory(Action<object> onDomainEventHandled)
        {
            _onDomainEventHandled = onDomainEventHandled;
        }

        public IEnumerable<IDomainEventHandler<TDomainEvent>> Create<TDomainEvent>() where TDomainEvent : IDomainEvent
        {
            yield return new FakeDomainEventHandler<TDomainEvent>(_onDomainEventHandled as Action<TDomainEvent>);
        }

        public void Release(object domainEventHandler)
        {
        }
    }
}