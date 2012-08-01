using NUnit.Framework;
using Shouldly;

namespace Core.Tests.Domain.Identities
{
    public abstract class base_when_comparing_entities_of_the_different_type_and_of_the_same_id<TId>
    {
        private TestEntity<TId> _entity;
        private AnotherTestEntity<TId> _anotherEntity;

        protected abstract TId GetId();

        [SetUp]
        public void Context()
        {
            _entity = new TestEntity<TId>(GetId());
            _anotherEntity = new AnotherTestEntity<TId>(GetId());
        }

        [Test]
        public void entity_equals_another_entity()
        {
            _entity.Equals(_anotherEntity).ShouldBe(false);
        }
    }
}