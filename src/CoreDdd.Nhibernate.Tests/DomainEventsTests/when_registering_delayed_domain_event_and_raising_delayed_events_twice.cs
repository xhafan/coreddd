using CoreDdd.Domain.Events;
using CoreDdd.TestHelpers.DomainEvents;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_registering_delayed_domain_event_and_raising_delayed_events_twice
    {
        private TestEntityWithDomainEvent _entity;
        private int _numberOfHandledDomainEvents;

        [SetUp]
        public void Context()
        {
            var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _numberOfHandledDomainEvents++);
            DomainEvents.Initialize(domainEventHandlerFactory, isDelayedDomainEventHandlingEnabled: true);
            DomainEvents.ResetDelayedEventsStorage();

            _entity = new TestEntityWithDomainEvent();
            _entity.BehaviouralMethodWithRaisingDomainEvent();

            DomainEvents.RaiseDelayedEvents();

            DomainEvents.ResetDelayedEventsStorage();
            DomainEvents.RaiseDelayedEvents();
        }

        [Test]
        public void domain_event_is_not_handled_again()
        {
            _numberOfHandledDomainEvents.ShouldBe(1);
        }
    }
}