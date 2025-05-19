using CoreDdd.Nhibernate.TestHelpers;
using IntegrationTestsShared;
using IntegrationTestsShared.EntitiesWithNonIntegerId;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.EntitiesWithNonIntegerIdPersistence
{
    [TestFixture]
    public class when_persisting_entity_with_long_id : BasePersistenceTest
    {
        [Test]
        public void entity_with_long_id_can_be_persisted_and_retrieved()
        {
            var entityWithLongId = new EntityWithLongId();

            UnitOfWork.Save<EntityWithLongId, long>(entityWithLongId);
            UnitOfWork.Clear();

            var fetchedEntityWithLongId = UnitOfWork.Get<EntityWithLongId, long>(entityWithLongId.Id);

            (fetchedEntityWithLongId == entityWithLongId).ShouldBeTrue();
            (fetchedEntityWithLongId.Id == entityWithLongId.Id).ShouldBeTrue();
        }
    }
}