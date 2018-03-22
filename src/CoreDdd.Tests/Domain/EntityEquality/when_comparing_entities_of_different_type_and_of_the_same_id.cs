using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Domain.EntityEquality
{
    [TestFixture(TypeArgs = new[] { typeof(int), typeof(GenerateIdValueForIntegerIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(long), typeof(GenerateIdValueForLongIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(string), typeof(GenerateIdValueForStringIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(CompositeId), typeof(GenerateIdValueForCompositeIdIdTypeSpecification) })]
    public class when_comparing_entities_of_different_type_and_of_the_same_id<TId, TGenerateIdValueForIdTypeSpecification>
        where TGenerateIdValueForIdTypeSpecification : IGenerateIdValueForIdTypeSpecification<TId>, new()
    {
        private TGenerateIdValueForIdTypeSpecification _specification;
        private TestEntity<TId> _entity;
        private AnotherTestEntity<TId> _anotherEntity;

        [SetUp]
        public void Context()
        {
            _specification = new TGenerateIdValueForIdTypeSpecification();

            _entity = new TestEntity<TId>(_specification.GetIdForEntityOne());
            _anotherEntity = new AnotherTestEntity<TId>(_specification.GetIdForEntityOne());
        }

        [Test]
        public void entity_does_not_equal_another_entity()
        {
            _entity.Equals(_anotherEntity).ShouldBe(false);
        }

        [Test]
        public void entity_does_not_equal_another_entity_using_equality_operator()
        {
            (_entity == _anotherEntity).ShouldBe(false);
        }

        [Test]
        public void entity_does_not_equal_another_entity_using_inequality_operator()
        {
            (_entity != _anotherEntity).ShouldBe(true);
        }
    }
}