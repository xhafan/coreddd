using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Domain.EntityEquality
{
    // todo: test Ids of type struct or class
    [TestFixture(TypeArgs = new[] { typeof(int) })]
    [TestFixture(TypeArgs = new[] { typeof(long) })]
    [TestFixture(TypeArgs = new[] { typeof(string) })]
    public class when_comparing_transient_entities_of_the_same_type_and_of_the_same_id<TId>
    {
        private TestEntity<TId> _entityOne;
        private TestEntity<TId> _entityTwo;

        [SetUp]
        public void Context()
        {
            _entityOne = new TestEntity<TId>();
            _entityTwo = new TestEntity<TId>();
        }

        [Test]
        public void two_transient_entities_are_not_equal()
        {
            _entityOne.Equals(_entityTwo).ShouldBe(false);
        }

        [Test]
        public void two_transient_entity_hash_codes_are_not_equal()
        {
            (_entityOne.GetHashCode() == _entityTwo.GetHashCode()).ShouldBe(false);
        }

        [Test]
        public void two_transient_entities_are_not_equal_using_quality_operator()
        {
            (_entityOne == _entityTwo).ShouldBe(false);
        }

        [Test]
        public void two_transient_entities_are_not_equal_using_inequality_operator()
        {
            (_entityOne != _entityTwo).ShouldBe(true);
        }
    }
}