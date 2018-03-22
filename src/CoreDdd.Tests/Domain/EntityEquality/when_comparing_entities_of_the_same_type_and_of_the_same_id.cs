using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Domain.EntityEquality
{
    [TestFixture(TypeArgs = new[] { typeof(int), typeof(GenerateIdValueForIntegerIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(long), typeof(GenerateIdValueForLongIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(string), typeof(GenerateIdValueForStringIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(CompositeId), typeof(GenerateIdValueForCompositeIdIdTypeSpecification) })]
    public class when_comparing_entities_of_the_same_type_and_of_the_same_id<TId, TGenerateIdValueForIdTypeSpecification>
        where TGenerateIdValueForIdTypeSpecification : IGenerateIdValueForIdTypeSpecification<TId>, new()
    {
        private TGenerateIdValueForIdTypeSpecification _specification;
        private TestEntity<TId> _entityOne;
        private TestEntity<TId> _entityTwo;

        [SetUp]
        public void Context()
        {
            _specification = new TGenerateIdValueForIdTypeSpecification();

            _entityOne = new TestEntity<TId>(_specification.GetIdForEntityOne());
            _entityTwo = new TestEntity<TId>(_specification.GetIdForEntityOne());
        }

        [Test]
        public void entities_of_the_same_type_and_id_are_equal()
        {
            _entityOne.Equals(_entityTwo).ShouldBe(true);
        }

        [Test]
        public void entities_of_the_same_type_and_id_have_the_same_hash_code()
        {
            (_entityOne.GetHashCode() == _entityTwo.GetHashCode()).ShouldBe(true);
        }

        [Test]
        public void entities_of_the_same_type_and_id_are_equal_using_equality_operator()
        {
            (_entityOne == _entityTwo).ShouldBe(true);
        }

        [Test]
        public void entities_of_the_same_type_and_id_are_equal_using_inequality_operator()
        {
            (_entityOne != _entityTwo).ShouldBe(false);
        }
    }
}
