using System;
using CoreDdd.Domain.Events;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests;

[TestFixture]
public class when_raising_delayed_events_without_initialization
{
    [SetUp]
    public void Context()
    {
        DomainEvents.Initialize(null!);
    }

    [Test]
    public void raising_domain_events_is_handled_gracefully()
    {
        var ex = Should.Throw<Exception>(DomainEvents.RaiseDelayedEvents);
            
        ex.Message.ShouldContain("not been initialized");
    }
}