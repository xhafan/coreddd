using CoreDdd.Nhibernate.TestHelpers;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.EntityProxyEquality
{
    [TestFixture]
    public class when_comparing_derived_entity_with_parent_entity_proxy : BasePersistenceTest
    {
        [Test]
        public void derived_entity_and_its_parent_entity_proxy_are_equal()
        {
            var derivedEntity = new DerivedEqualityEntity();

            UnitOfWork.Save(derivedEntity);
            UnitOfWork.Clear();

            var parentEntityProxy = UnitOfWork.Load<EqualityEntity>(derivedEntity.Id);

            (derivedEntity == parentEntityProxy).ShouldBeTrue();
        }
    }
}