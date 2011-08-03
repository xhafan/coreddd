using Core.Commons;
using Core.Utilities.Extensions;

namespace Core.Domain.Events
{
    public static class DomainEvents
    {
        public static void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            // todo: test this?
            var domainEventHandlers = IoC.ResolveAll<IDomainEventHandler<TDomainEvent>>();
            domainEventHandlers.Each(e => e.Handle(domainEvent));
        }
    }
}
