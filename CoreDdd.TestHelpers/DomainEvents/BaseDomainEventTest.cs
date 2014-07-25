using CoreDdd.Domain.Events;
using CoreIoC;
using CoreTest;
using Rhino.Mocks;

namespace CoreDdd.TestHelpers.DomainEvents
{
    public class BaseDomainEventTest<TDomainEvent> : BaseTest, IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        protected TDomainEvent RaisedDomainEvent;

        public void Handle(TDomainEvent domainEvent)
        {
            RaisedDomainEvent = domainEvent;
        }

        protected BaseDomainEventTest()
        {
            var container = Stub<IContainer>();
            container.Stub(x => x.ResolveAll<IDomainEventHandler<TDomainEvent>>()).Return(new[]{this});
            IoC.Initialize(container);
        }
    }
}