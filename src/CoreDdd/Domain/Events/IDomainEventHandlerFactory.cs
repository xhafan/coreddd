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

        /// <summary>
        /// Releases a domain event handler instance previously created by <see cref="Create{TDomainEvent}"/> method.
        /// This is needed by Castle Windsor IoC container, other IoC containers (e.g. Ninject, ServiceProvider) don't support it.
        /// </summary>
        /// <param name="domainEventHandler"></param>
        void Release(object domainEventHandler);
    }
}