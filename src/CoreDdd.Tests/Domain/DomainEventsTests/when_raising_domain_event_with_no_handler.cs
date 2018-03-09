using CoreDdd.Domain.Events;
using CoreIoC;
using CoreUtils;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Domain.DomainEventsTests
{
    [TestFixture]
    public class when_raising_domain_event_with_no_handler
    {
        private CoreException _exception;

        public class TestDomainEvent : IDomainEvent
        {            
        }

        [SetUp]
        public void Context()
        {
            var container = A.Fake<IContainer>();
            A.CallTo(() => container.ResolveAll<IDomainEventHandler<TestDomainEvent>>()).Returns(new IDomainEventHandler<TestDomainEvent>[0]);
            IoC.Initialize(container);

            _exception = Should.Throw<CoreException>(() => DomainEvents.RaiseEvent(new TestDomainEvent()));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ShouldContain("No domain event handler for ");
        }

    }
}