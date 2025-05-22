using System;
using CoreDdd.Domain.Events;
using CoreDdd.TestHelpers.DomainEvents;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests;

[TestFixture]
public class when_raising_delayed_events_without_initialization
{
    [SetUp]
    public void Context()
    {
        var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(_ => { });
        DomainEvents.Initialize(domainEventHandlerFactory, isDelayedDomainEventHandlingEnabled: true);
        
        DomainEvents.ResetDelayedEventsStorage();
        
        DomainEvents.RaiseEvent(new TestDomainEvent());
        
        _simulateDomainEventsNotInitialized();
        return;

        void _simulateDomainEventsNotInitialized()
        {
            DomainEvents.Initialize(null!, isDelayedDomainEventHandlingEnabled: true);
        }
    }

    [Test]
    public void raising_domain_events_is_handled_gracefully()
    {
        var ex = Should.Throw<Exception>(DomainEvents.RaiseDelayedEvents);
            
        ex.Message.ShouldContain("not been initialized");
    }
}