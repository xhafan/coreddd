using System.Collections.Generic;
using CoreDdd.Domain.Events;
using Ninject;
using Ninject.Syntax;

namespace CoreDdd.Register.Ninject
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        private readonly IResolutionRoot _ninjectIoCContainer;

        public DomainEventHandlerFactory(IResolutionRoot ninjectIoCContainer)
        {
            _ninjectIoCContainer = ninjectIoCContainer;
        }

        public IEnumerable<IDomainEventHandler<TDomainEvent>> Create<TDomainEvent>() where TDomainEvent : IDomainEvent
        {
            return _ninjectIoCContainer.Get<IEnumerable<IDomainEventHandler<TDomainEvent>>>();
        }

        public void Release(object domainEventHandler)
        {
            // do nothing - Ninject does not have a concept of releasing components like Castle Windsor
        }
    }
}