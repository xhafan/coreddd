using CoreDdd.Domain.Events;
using CoreDdd.TestHelpers.DomainEvents;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_registering_delayed_domain_events_and_raising_them_later
    {
        private TestEntityWithDomainEvent _entity;
        private TestDomainEvent _raisedDomainEvent;

        [SetUp]
        public void Context()
        {
            _raisedDomainEvent = null;

            var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
            DomainEvents.Initialize(domainEventHandlerFactory, isDelayedDomainEventHandlingEnabled: true);
            DomainEvents.ResetDelayedEventsStorage();

            _entity = new TestEntityWithDomainEvent();


            _entity.BehaviouralMethodWithRaisingDomainEvent();
        }

        [Test]
        public void domain_event_is_not_handled()
        {
            _raisedDomainEvent.ShouldBeNull();
        }

        [Test]
        public void domain_event_is_handled_after_registered_delayed_events_are_raised()
        {
            DomainEvents.RaiseDelayedEvents();

            _raisedDomainEvent.ShouldNotBeNull();
        }
    }
}