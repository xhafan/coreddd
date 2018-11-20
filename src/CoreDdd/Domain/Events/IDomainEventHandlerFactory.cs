using System.Collections.Generic;

namespace CoreDdd.Domain.Events
{
    /// <summary>
    /// A domain event handler factory.
    /// Usually implemented auto-magically by an IoC container.
    /// </summary>
    public interface IDomainEventHandlerFactory
    {
        /// <summary>
        /// Creates 0 or multiple domain event handlers for a given domain event.
        /// </summary>
        /// <typeparam name="TDomainEvent"></typeparam>
        /// <returns></returns>
        IEnumerable<IDomainEventHandler<TDomainEvent>> Create<TDomainEvent>() 
            where TDomainEvent : IDomainEvent;

        void Release(object domainEventHandler);
    }
}