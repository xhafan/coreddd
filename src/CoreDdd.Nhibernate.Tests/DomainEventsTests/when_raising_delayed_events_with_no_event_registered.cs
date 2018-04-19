using CoreDdd.Domain.Events;
using CoreIoC;
using CoreUtils.Storages;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_raising_delayed_events_with_no_event_registered
    {
        [Test]
        public void raising_domain_events_is_handled_gracefully()
        {
            _resetDelayedDomainEventHandlingActionsStorage();

            DomainEvents.RaiseDelayedEvents(eventHandlingSurroundingAction => eventHandlingSurroundingAction());


            void _resetDelayedDomainEventHandlingActionsStorage()
            {
                _getDelayedDomainEventHandlingActionsStorage().Set(null);
            }
        }

        private IStorage<DelayedDomainEventHandlingActions> _getDelayedDomainEventHandlingActionsStorage()
        {
            return IoC.Resolve<IStorage<DelayedDomainEventHandlingActions>>();
        }
    }
}