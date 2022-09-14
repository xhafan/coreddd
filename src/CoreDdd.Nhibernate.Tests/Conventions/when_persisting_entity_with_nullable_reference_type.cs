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
    public class when_persisting_entity_with_nullable_reference_type : BasePersistenceTest
    {
        [Test]
        public void entity_is_persisted_with_null_value()
        {
            var entity = new EntityWithNullableText(null);

            UnitOfWork.Save(entity);
            UnitOfWork.Clear();

            entity = UnitOfWork.Get<EntityWithNullableText>(entity.Id);

            entity.ShouldNotBeNull();
            entity.Text.ShouldBe(null);
        }

        [Test]
        public void entity_is_persisted_with_not_null_value()
        {
            var entity = new EntityWithNullableText("text");

            UnitOfWork.Save(entity);
            UnitOfWork.Clear();

            entity = UnitOfWork.Get<EntityWithNullableText>(entity.Id);

            entity.ShouldNotBeNull();
            entity.Text.ShouldBe("text");
        }
    }
}
#endif