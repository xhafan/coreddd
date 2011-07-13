using System;
using Core.Domain.Events;
using NServiceBus;

namespace EmailMaker.Domain.Messages
{
    [Serializable]
    public class GenericDomainEventMessage<TDomainEvent> : IMessage where TDomainEvent : IDomainEvent
    {
        private TDomainEvent _domainEvent;

        public GenericDomainEventMessage(TDomainEvent domainEvent)
        {
            _domainEvent = domainEvent;
        }
    }
}