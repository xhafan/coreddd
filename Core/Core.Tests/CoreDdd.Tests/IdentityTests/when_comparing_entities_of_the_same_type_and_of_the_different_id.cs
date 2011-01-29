using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.IdentityTests
{
    [TestFixture]
    public class when_comparing_entities_of_the_same_type_and_of_the_different_id
    {
        private Entity _entityOne;
        private Entity _entityTwo;

        [SetUp]
        public void Context()
        {
            _entityOne = new Entity(11);
            _entityTwo = new Entity(12);
        }

        [Test]
        public void entity_one_equals_entity_two()
        {
            _entityOne.Equals(_entityTwo).ShouldBe(false);
            (_entityOne.GetHashCode() == _entityTwo.GetHashCode()).ShouldBe(false);
        }

        [Test]
        public void entity_one_equals_entity_two_by_operator()
        {
            (_entityOne == _entityTwo).ShouldBe(false);
        }

        [Test]
        public void entity_one_equals_entity_two_by_negated_operator()
        {
            (_entityOne != _entityTwo).ShouldBe(true);
        }

    }
}