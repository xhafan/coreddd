using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.EntityWithNonIntegerIdPersistence
{
    [TestFixture]
    public class when_persisting_entity_with_long_id : BasePersistenceTest
    {
        [Test]
        public void entity_with_long_id_can_be_persisted_and_retreieved()
        {
            var entityWithLongId = new EntityWithLongId();

            SaveGeneric<EntityWithLongId, long>(entityWithLongId);
            Clear();

            var fetchedEntityWithLongId = GetGeneric<EntityWithLongId, long>(entityWithLongId.Id);

            (fetchedEntityWithLongId == entityWithLongId).ShouldBeTrue();
            (fetchedEntityWithLongId.Id == entityWithLongId.Id).ShouldBeTrue();
        }
    }
}