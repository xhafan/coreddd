using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Domain.EntityEquality
{
    [TestFixture(TypeArgs = new[] { typeof(int), typeof(GenerateIdValueForIntegerIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(long), typeof(GenerateIdValueForLongIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(string), typeof(GenerateIdValueForStringIdTypeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(CompositeId), typeof(GenerateIdValueForCompositeIdIdTypeSpecification) })]
    public class when_adding_entity_into_a_hashset<TId, TGenerateIdValueForIdTypeSpecification>
        where TGenerateIdValueForIdTypeSpecification : IGenerateIdValueForIdTypeSpecification<TId>, new()
    {
        private TGenerateIdValueForIdTypeSpecification _specification;
        private TestEntity<TId> _transientEntityOne;
        private HashSet<TestEntity<TId>> _entities;

        [SetUp]
        public void Context()
        {
            _specification = new TGenerateIdValueForIdTypeSpecification();

            _transientEntityOne = new TestEntity<TId>();
            _entities = new HashSet<TestEntity<TId>>(new[] { _transientEntityOne });
        }

        [Test]
        public void hashset_contains_transient_entity()
        {
            _entities.Contains(_transientEntityOne).ShouldBe(true);
        }

        [Test]
        public void changing_transient_entity_to_persitent_entity_and_checking_the_hashset()
        {
            var id = _specification.GetIdForEntityOne();
            _transientEntityOne.SetId(id);

            _entities.Contains(_transientEntityOne).ShouldBe(true);
        }

    }
}