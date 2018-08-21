using CoreDdd.Domain.Events;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.TestHelpers.DomainEvents;
using CoreIoC;
using CoreUtils.Storages;
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
            var storageFactory = IoC.Resolve<IStorageFactory>();
            DomainEvents.InitializeWithDelayedDomainEventHandling(domainEventHandlerFactory, storageFactory);            
            _resetDelayedDomainEventHandlingItemsStorage();

            _entity = new TestEntityWithDomainEvent();


            _entity.BehaviouralMethodWithRaisingDomainEvent();


            void _resetDelayedDomainEventHandlingItemsStorage()
            {
                _GetDelayedDomainEventHandlingItemsStorage().Set(null);
            }
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

        [Test]
        public void delayed_domain_events_are_cleared_after_raising_them()
        {
            DomainEvents.RaiseDelayedEvents();

            _GetDelayedDomainEventHandlingItemsStorage().Get().ShouldBeEmpty();
        }

        private IStorage<DelayedDomainEventHandlingItems> _GetDelayedDomainEventHandlingItemsStorage()
        {
            return IoC.Resolve<IStorage<DelayedDomainEventHandlingItems>>();
        }
    }
}