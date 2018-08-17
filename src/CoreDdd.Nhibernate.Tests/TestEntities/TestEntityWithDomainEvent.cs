using CoreDdd.Domain;
using CoreDdd.Domain.Events;

namespace CoreDdd.Nhibernate.Tests.TestEntities
{
    public class TestEntityWithDomainEvent : Entity, IAggregateRoot
    {
        public virtual void BehaviouralMethodWithRaisingDomainEvent()
        {
            DomainEvents.RaiseEvent(new TestDomainEvent());
        }
    }
}