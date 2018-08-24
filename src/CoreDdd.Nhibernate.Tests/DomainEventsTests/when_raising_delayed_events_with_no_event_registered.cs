using CoreDdd.Domain.Events;
using CoreIoC;
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
            DomainEvents.Initialize(domainEventHandlerFactory, isDelayedDomainEventHandlingEnabled: true);

            DomainEvents.ResetDelayedEventsStorage();
        }

        [Test]
        public void raising_domain_events_is_handled_gracefully()
        {
            DomainEvents.RaiseDelayedEvents();
        }
    }
}