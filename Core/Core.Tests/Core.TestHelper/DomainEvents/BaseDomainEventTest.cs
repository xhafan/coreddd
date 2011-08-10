using Castle.Windsor;
using Core.Commons;
using Core.Domain.Events;
using Rhino.Mocks;

namespace Core.TestHelper.DomainEvents
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