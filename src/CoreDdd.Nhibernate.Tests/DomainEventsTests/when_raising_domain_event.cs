using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.DomainEventsTests
{
    [TestFixture]
    public class when_raising_domain_event
    {
        private TestEntityWithDomainEvent _entity;

        [SetUp]
        public void Context()
        {
            TestDomainEventHandler.ResetDomainEventWasHandledFlag();
            _entity = new TestEntityWithDomainEvent();


            _entity.BehaviourMethodWithRaisingDomainEvent();
        }

        [Test]
        public void domain_event_is_handled()
        {
            TestDomainEventHandler.DomainEventWasHandled.ShouldBeTrue();
        }
   }
}