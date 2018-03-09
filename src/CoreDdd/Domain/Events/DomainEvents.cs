using CoreIoC;
using CoreUtils;
using CoreUtils.Extensions;

namespace CoreDdd.Domain.Events
{
    public static class DomainEvents
    {
        public static void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var domainEventHandlers = IoC.ResolveAll<IDomainEventHandler<TDomainEvent>>();
            Guard.Hope(domainEventHandlers.Length > 0, "No domain event handler for " + domainEvent);
            domainEventHandlers.Each(e => e.Handle(domainEvent));
        }
    }
}
