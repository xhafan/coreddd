using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests.EntityProxyEquality
{
    [TestFixture]
    public class when_comparing_derived_entity_with_parent_entity_proxy : BasePersistenceTest
    {
        [Test]
        public void derived_entity_and_its_parent_entity_proxy_are_equal()
        {
            var derivedEntity = new DerivedEqualityEntity();

            Save(derivedEntity);
            Clear();

            var parentEntityProxy = Load<EqualityEntity>(derivedEntity.Id);

            (derivedEntity == parentEntityProxy).ShouldBeTrue();
        }
    }
}