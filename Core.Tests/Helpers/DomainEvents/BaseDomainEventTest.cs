using Castle.Windsor;
using Core.Domain.Events;
using Core.Infrastructure;
using Rhino.Mocks;

namespace Core.Tests.Helpers.DomainEvents
{
    public class BaseDomainEventTest<TDomainEvent> : IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        protected TDomainEvent RaisedDomainEvent;

        public void Handle(TDomainEvent domainEvent)
        {
            RaisedDomainEvent = domainEvent;
        }

        protected BaseDomainEventTest()
        {
            var container = MockRepository.GenerateStub<IWindsorContainer>();
            container.Stub(a => a.ResolveAll<IDomainEventHandler<TDomainEvent>>()).Return(new[]{this});
            IoC.Initialize(container);
        }
    }
}