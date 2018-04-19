using CoreDdd.Domain;
using CoreDdd.Domain.Events;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    public class TestEntityWithDomainEvent : Entity, IAggregateRoot
    {
        public virtual void BehaviouralMethodWithRaisingDomainEvent()
        {
            DomainEvents.RaiseEvent(new TestDomainEvent());
        }
    }
}