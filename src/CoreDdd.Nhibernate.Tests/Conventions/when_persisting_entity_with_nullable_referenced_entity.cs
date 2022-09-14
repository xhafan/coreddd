#if NET6_0_OR_GREATER
#nullable enable
using CoreDdd.Nhibernate.TestHelpers;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Conventions
{
    [TestFixture]
    public class when_persisting_entity_with_nullable_referenced_entity : BasePersistenceTest
    {
        [Test]
        public void entity_is_persisted_with_null_referenced_entity()
        {
            var entity = new EntityWithNullableReference(null);

            UnitOfWork.Save(entity);
            UnitOfWork.Clear();

            entity = UnitOfWork.Get<EntityWithNullableReference>(entity.Id);

            entity.ShouldNotBeNull();
            entity.ReferencedEntity.ShouldBe(null);
        }

        [Test]
        public void entity_is_persisted_with_not_null_referenced_entity()
        {
            var entityWithNullableText = new EntityWithNullableText("text");
            UnitOfWork.Save(entityWithNullableText);

            var entity = new EntityWithNullableReference(entityWithNullableText);

            UnitOfWork.Save(entity);
            UnitOfWork.Clear();

            entity = UnitOfWork.Get<EntityWithNullableReference>(entity.Id);

            entity.ShouldNotBeNull();
            entity.ReferencedEntity.ShouldBe(entityWithNullableText);
        }
    }
}
#endif