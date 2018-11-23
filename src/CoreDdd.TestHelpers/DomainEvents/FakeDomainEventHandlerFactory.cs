using System;
using System.Collections.Generic;
using CoreDdd.Domain.Events;

namespace CoreDdd.TestHelpers.DomainEvents
{
    /// <summary>
    /// Fake domain event handler factory to enable unit testing of domain events.
    /// </summary>
    public class FakeDomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        private readonly Action<object> _onDomainEventHandled;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="onDomainEventHandled">An action called with the raised domain event as a parameter when the domain event is handled.</param>
        public FakeDomainEventHandlerFactory(Action<object> onDomainEventHandled)
        {
            _onDomainEventHandled = onDomainEventHandled;
        }

        /// <summary>
        /// Returns a collection of domain event handlers for a given domain event.
        /// </summary>
        /// <typeparam name="TDomainEvent">A domain event type</typeparam>
        /// <returns>A collection of domain event handlers</returns>
        public IEnumerable<IDomainEventHandler<TDomainEvent>> Create<TDomainEvent>() where TDomainEvent : IDomainEvent
        {
            yield return new FakeDomainEventHandler<TDomainEvent>(_onDomainEventHandled as Action<TDomainEvent>);
        }
        
        /// <summary>
        /// Cleans up the created domain event handler.
        /// </summary>
        /// <param name="domainEventHandler">An instance of domain event handler</param>
        public void Release(object domainEventHandler)
        {
        }
    }
}