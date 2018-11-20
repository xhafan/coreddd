using System;

namespace CoreDdd.Domain.Events
{
    internal class DelayedDomainEventHandlingItem
    {
        public DelayedDomainEventHandlingItem(object domainEventHandler, Action domainEventHandlingAction)
        {
            DomainEventHandler = domainEventHandler;
            DomainEventHandlingAction = domainEventHandlingAction;
        }

        public object DomainEventHandler { get; }
        public Action DomainEventHandlingAction { get; }        
    }
}