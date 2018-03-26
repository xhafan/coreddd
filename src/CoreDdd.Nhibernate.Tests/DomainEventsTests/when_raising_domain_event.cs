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
            TestDomainEventHandler.ResetDomainEventWasHandled();
            _entity = new TestEntityWithDomainEvent();


            _entity.BehaviourRaisingDomainEvent();
        }

        [Test]
        public void domain_event_was_handled()
        {
            TestDomainEventHandler.DomainEventWasHandled.ShouldBeTrue();
        }
   }
}