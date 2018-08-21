using CoreDdd.Domain.Events;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.TestHelpers.DomainEvents;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_raising_domain_event
    {
        private TestEntityWithDomainEvent _entity;
        private TestDomainEvent _raisedDomainEvent;

        [SetUp]
        public void Context()
        {
            var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
            DomainEvents.Initialize(domainEventHandlerFactory);

            _entity = new TestEntityWithDomainEvent();
            

            _entity.BehaviouralMethodWithRaisingDomainEvent();
        }

        [Test]
        public void domain_event_is_handled()
        {
            _raisedDomainEvent.ShouldNotBeNull();
        }
   }
}