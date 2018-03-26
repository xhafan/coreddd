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

        [SetUp]
        public void Context()
        {
            _setupContainerToResolveDomainEventHandlers();
            _testDomainEvent = new TestDomainEvent();


            DomainEvents.RaiseEvent(_testDomainEvent);


            void _setupContainerToResolveDomainEventHandlers()
            {
                var container = A.Fake<IContainer>();
                _testDomainHandler = A.Fake<IDomainEventHandler<TestDomainEvent>>();
                A.CallTo(() => container.ResolveAll<IDomainEventHandler<TestDomainEvent>>()).Returns(new[] { _testDomainHandler });
                IoC.Initialize(container);
            }
        }


        [Test]
        public void event_was_handled()
        {
            A.CallTo(() =>_testDomainHandler.Handle(_testDomainEvent)).MustHaveHappened();
        }

        public class TestDomainEvent : IDomainEvent
        {
        }
    }
}