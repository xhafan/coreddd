using CoreDdd.Domain;
using CoreDdd.Domain.Events;

namespace IntegrationTestsShared.TestEntities
{
    public class TestEntityWithDomainEvent : Entity, IAggregateRoot
    {
        public virtual void BehaviouralMethodWithRaisingDomainEvent()
        {
            DomainEvents.RaiseEvent(new TestDomainEvent());
        }
    }
}