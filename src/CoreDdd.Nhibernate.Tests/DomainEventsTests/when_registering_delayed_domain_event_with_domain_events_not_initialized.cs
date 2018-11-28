using System;
using CoreDdd.Domain.Events;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_registering_delayed_domain_events_with_domain_events_not_initialized_with_domain_event_handler_factory
    {
        [Test]
        public void raising_delayed_domain_event_throws_not_initialized()
        {
            _simulateDomainEventsNotInitialized();
            DomainEvents.ResetDelayedEventsStorage();
            
            var ex = Should.Throw<InvalidOperationException>(() => new TestEntityWithDomainEvent().BehaviouralMethodWithRaisingDomainEvent());

            ex.Message.ToLower().ShouldContain("DomainEvents.Initialize");

            
            void _simulateDomainEventsNotInitialized()
            {
                DomainEvents.Initialize(domainEventHandlerFactory: null, isDelayedDomainEventHandlingEnabled: true);
            }
        }
    }
}