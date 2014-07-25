using CoreDdd.Domain.Events;
using CoreIoC;
using CoreTest;
using CoreUtils;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreDdd.Tests.Domain.DomainEventsTests
{
    [TestFixture]
    public class when_raising_domain_event_with_no_handler : BaseTest
    {
        private CoreException _exception;

        public class TestDomainEvent : IDomainEvent
        {            
        }

        [SetUp]
        public void Context()
        {
            var container = Stub<IContainer>();
            container.Stub(x => x.ResolveAll<IDomainEventHandler<TestDomainEvent>>()).Return(new IDomainEventHandler<TestDomainEvent>[0]);
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