using System;
using System.Collections.Generic;
using CoreDdd.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDdd.Register.DependencyInjection
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IDomainEventHandler<TDomainEvent>> Create<TDomainEvent>() 
            where TDomainEvent : IDomainEvent
        {
            return _serviceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
        }

        public void Release(object domainEventHandler)
        {
        }
    }
}