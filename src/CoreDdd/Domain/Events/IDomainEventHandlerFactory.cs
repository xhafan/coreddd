using System.Collections.Generic;

namespace CoreDdd.Domain.Events
{
    public interface IDomainEventHandlerFactory
    {
        IEnumerable<IDomainEventHandler<TDomainEvent>> Create<TDomainEvent>() 
            where TDomainEvent : IDomainEvent;

        void Release(object domainEventHandler);
    }
}