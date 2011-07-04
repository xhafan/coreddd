using Core.Commons;

namespace Core.Domain.Events
{
    public static class DomainEvents
    {
        public static void RaiseEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var domainEventHandler = IoC.Resolve<IDomainEventHandler<TDomainEvent>>();
            domainEventHandler.Handle(domainEvent);
        }
    }
}
