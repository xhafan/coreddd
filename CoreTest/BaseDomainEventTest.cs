using CoreDdd.Domain.Events;
using CoreIoC;
using FakeItEasy;

namespace CoreTest
{
    // todo: reevaluate if this is still needed
    public class BaseDomainEventTest<TDomainEvent> : BaseTest, IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        protected TDomainEvent RaisedDomainEvent;

        public void Handle(TDomainEvent domainEvent)
        {
            RaisedDomainEvent = domainEvent;
        }

        protected BaseDomainEventTest()
        {
            var container = A.Fake<IContainer>();
            A.CallTo(() => container.ResolveAll<IDomainEventHandler<TDomainEvent>>()).Returns(new[]{this});
            IoC.Initialize(container);
        }
    }
}