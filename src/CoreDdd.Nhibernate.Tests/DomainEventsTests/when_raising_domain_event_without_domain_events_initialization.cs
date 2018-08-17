using System;
using CoreDdd.Domain.Events;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_raising_domain_event_without_domain_events_initialization
    {
        [Test]
        public void raising_domain_event_throws_not_initialized()
        {
            _simulateDomainEventsNotInitialized();

            var ex = Should.Throw<InvalidOperationException>(() => new TestEntityWithDomainEvent().BehaviouralMethodWithRaisingDomainEvent());

            ex.Message.ToLower().ShouldContain("DomainEvents.Initialize");

            void _simulateDomainEventsNotInitialized()
            {
                DomainEvents.Initialize(domainEventHandlerFactory: null);
            }
        }
    }
}