using NUnit.Framework;
using Shouldly;

namespace Core.Tests.Domain.Identities
{
    public abstract class base_when_comparing_entities_of_the_same_type_and_of_the_different_id<TId>
    {
        private TestEntity<TId> _entityOne;
        private TestEntity<TId> _entityTwo;

        protected abstract TId GetIdOne();
        protected abstract TId GetIdTwo();

        [SetUp]
        public void Context()
        {
            _entityOne = new TestEntity<TId>(GetIdOne());
            _entityTwo = new TestEntity<TId>(GetIdTwo());
        }

        [Test]
        public void entity_one_does_not_equal_entity_two()
        {
            _entityOne.Equals(_entityTwo).ShouldBe(false);
            (_entityOne.GetHashCode() == _entityTwo.GetHashCode()).ShouldBe(false);
        }

        [Test]
        public void entity_one_does_not_equal_entity_two_by_operator()
        {
            (_entityOne == _entityTwo).ShouldBe(false);
        }

        [Test]
        public void entity_one_does_not_equal_entity_two_by_negated_operator()
        {
            (_entityOne != _entityTwo).ShouldBe(true);
        }

    }
}