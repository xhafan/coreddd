using NUnit.Framework;
using Shouldly;

namespace Core.Domain.Tests.IdentityTests
{
    [TestFixture]
    public class when_comparing_entities_of_the_different_type_and_of_the_same_id
    {
        private Entity _entity;
        private AnotherEntity _anotherEntity;

        [SetUp]
        public void Context()
        {
            _entity = new Entity(11);
            _anotherEntity = new AnotherEntity(11);
        }

        [Test]
        public void entity_equals_another_entity()
        {
            _entity.Equals(_anotherEntity).ShouldBe(false);
        }

        [Test]
        public void entity_equals_another_entity_by_operator()
        {
            (_entity == _anotherEntity).ShouldBe(false);
        }

        [Test]
        public void entity_equals_another_entity_by_negated_operator()
        {
            (_entity != _anotherEntity).ShouldBe(true);
        }

    }
}