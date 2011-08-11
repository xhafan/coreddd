using Castle.Windsor;
using Core.Commons;
using Core.Domain.Events;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Domain.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_raising_domain_event
    {
        public IDomainEventHandler<TestDomainEvent> _testDomainHandler;
        public TestDomainEvent _testDomainEvent;

        public class TestDomainEvent : IDomainEvent
        {            
        }

        [SetUp]
        public void Context()
        {
            var container = MockRepository.GenerateStub<IWindsorContainer>();
            _testDomainHandler = MockRepository.GenerateMock<IDomainEventHandler<TestDomainEvent>>();
            container.Stub(a => a.ResolveAll<IDomainEventHandler<TestDomainEvent>>()).Return(new[] { _testDomainHandler });
            IoC.Initialize(container);

            _testDomainEvent = new TestDomainEvent();
            DomainEvents.RaiseEvent(_testDomainEvent);
        }

        [Test]
        public void event_was_handled()
        {
            _testDomainHandler.AssertWasCalled(a => a.Handle(_testDomainEvent));
        }

    }
}