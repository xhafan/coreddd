using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Conventions
{
    [TestFixture]
    public class when_persisting_concrete_entity_derived_from_abstract_entity : BasePersistenceTest
    {
        [Test]
        public void concrete_entity_is_persisted()
        {
            var entity = new ConcreteEntity();

            Save(entity);
            Clear();

            entity = Get<ConcreteEntity>(entity.Id);

            entity.ShouldNotBeNull();
        }
    }
}