using Castle.Windsor;
using Core.Domain.Events;
using Core.Infrastructure;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Tests.Domain.DomainEventsTests
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