using CoreDdd.Domain.Events;
using CoreIoC;
using FakeItEasy;
using NUnit.Framework;

namespace CoreDdd.Tests.Domain.DomainEventsTests
{
    [TestFixture]
    public class when_raising_domain_event
    {
        private IDomainEventHandler<TestDomainEvent> _testDomainHandler;
        private TestDomainEvent _testDomainEvent;

        public class TestDomainEvent : IDomainEvent
        {            
        }

        [SetUp]
        public void Context()
        {
            var container = A.Fake<IContainer>();
            _testDomainHandler = A.Fake<IDomainEventHandler<TestDomainEvent>>();
            A.CallTo(() => container.ResolveAll<IDomainEventHandler<TestDomainEvent>>()).Returns(new[] { _testDomainHandler });
            IoC.Initialize(container);

            _testDomainEvent = new TestDomainEvent();
            DomainEvents.RaiseEvent(_testDomainEvent);
        }

        [Test]
        public void event_was_handled()
        {
            A.CallTo(() =>_testDomainHandler.Handle(_testDomainEvent)).MustHaveHappened();
        }

    }
}