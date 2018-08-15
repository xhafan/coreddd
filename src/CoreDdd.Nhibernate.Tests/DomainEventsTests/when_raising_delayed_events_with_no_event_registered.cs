using CoreDdd.Domain.Events;
using CoreIoC;
using CoreUtils.Storages;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_raising_delayed_events_with_no_event_registered
    {
        [SetUp]
        public void Context()
        {
            var domainEventHandlerFactory = IoC.Resolve<IDomainEventHandlerFactory>();
            var storageFactory = IoC.Resolve<IStorageFactory>();
            DomainEvents.InitializeWithDelayedDomainEventHandling(domainEventHandlerFactory, storageFactory);

            _resetDelayedDomainEventHandlingItemsStorage();

            void _resetDelayedDomainEventHandlingItemsStorage()
            {
                IoC.Resolve<IStorage<DelayedDomainEventHandlingItems>>().Set(null);
            }
        }

        [Test]
        public void raising_domain_events_is_handled_gracefully()
        {
            DomainEvents.RaiseDelayedEvents(eventHandlingSurroundingAction => eventHandlingSurroundingAction());
        }
    }
}