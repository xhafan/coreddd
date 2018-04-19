using CoreDdd.Domain.Events;
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

        [SetUp]
        public void Context()
        {
            DomainEvents.EnableDelayedDomainEventHandling();
            _resetDelayedDomainEventHandlingActionsStorage();
            TestDomainEventHandler.ResetDomainEventWasHandledFlag();

            _entity = new TestEntityWithDomainEvent();


            _entity.BehaviouralMethodWithRaisingDomainEvent();


            void _resetDelayedDomainEventHandlingActionsStorage()
            {
                _getDelayedDomainEventHandlingActionsStorage().Set(null);
            }
        }

        [Test]
        public void domain_event_is_not_handled()
        {
            TestDomainEventHandler.DomainEventWasHandled.ShouldBeFalse();
        }

        [Test]
        public void domain_event_is_handled_after_registered_delayed_events_are_raised()
        {
            DomainEvents.RaiseDelayedEvents(eventHandlingSurroundingAction => eventHandlingSurroundingAction());

            TestDomainEventHandler.DomainEventWasHandled.ShouldBeTrue();
        }

        [Test]
        public void delayed_domain_events_are_cleared_after_raising_them()
        {
            DomainEvents.RaiseDelayedEvents(action => action());

            _getDelayedDomainEventHandlingActionsStorage().Get().ShouldBeEmpty();
        }

        private IStorage<DelayedDomainEventHandlingActions> _getDelayedDomainEventHandlingActionsStorage()
        {
            return IoC.Resolve<IStorage<DelayedDomainEventHandlingActions>>();
        }
    }
}