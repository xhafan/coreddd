using Castle.Windsor;
using Core.Commons;
using Core.Domain.Events;
using Core.Utilities;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace Core.Domain.Tests.DomainEventsTests
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
            var container = MockRepository.GenerateStub<IWindsorContainer>();
            container.Stub(a => a.ResolveAll<IDomainEventHandler<TestDomainEvent>>()).Return(new IDomainEventHandler<TestDomainEvent>[0]);
            IoC.Initialize(container);

            _exception = Assert.Throws<CoreException>(() => DomainEvents.RaiseEvent(new TestDomainEvent()));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ShouldContain("No domain event handler for ");
        }

    }
}