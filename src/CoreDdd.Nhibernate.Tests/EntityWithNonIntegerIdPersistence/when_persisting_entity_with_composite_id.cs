using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.EntityWithNonIntegerIdPersistence
{
    [TestFixture]
    public class when_persisting_entity_with_composite_id : BasePersistenceTest
    {
        [Test]
        public void entity_with_composite_id_can_be_persisted_and_retreieved()
        {
            var entityWithCompositeId = new EntityWithCompositeId(new CompositeId(23, "string id"));

            UnitOfWork.Save<EntityWithCompositeId, CompositeId>(entityWithCompositeId);
            UnitOfWork.Clear();

            var fetchedEntityWithCompositeId = UnitOfWork.Get<EntityWithCompositeId, CompositeId>(entityWithCompositeId.Id);

            (fetchedEntityWithCompositeId == entityWithCompositeId).ShouldBeTrue();
            (fetchedEntityWithCompositeId.Id.IdOne == entityWithCompositeId.Id.IdOne).ShouldBeTrue();
            (fetchedEntityWithCompositeId.Id.IdTwo == entityWithCompositeId.Id.IdTwo).ShouldBeTrue();
        }
    }
}