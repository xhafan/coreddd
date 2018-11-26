using CoreDdd.Nhibernate.TestHelpers;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.EntityWithNonIntegerIdPersistence
{
    [TestFixture]
    public class when_persisting_entity_with_string_id : BasePersistenceTest
    {
        [Test]
        public void entity_with_string_id_can_be_persisted_and_retreieved()
        {
            var entityWithStringId = new EntityWithStringId("string id");

            UnitOfWork.Save<EntityWithStringId, string>(entityWithStringId);
            UnitOfWork.Clear();

            var fetchedEntityWithStringId = UnitOfWork.Get<EntityWithStringId, string>(entityWithStringId.Id);

            (fetchedEntityWithStringId == entityWithStringId).ShouldBeTrue();
            (fetchedEntityWithStringId.Id == entityWithStringId.Id).ShouldBeTrue();
        }
    }
}