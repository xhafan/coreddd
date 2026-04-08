#if NET8_0_OR_GREATER
using CoreDdd.Nhibernate.TestHelpers;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Conventions;

[TestFixture]
public class when_persisting_get_only_nullable_reference_type_property : BasePersistenceTest
{
    [Test]
    public void null_value_is_persisted()
    {
        var entity = new EntityWithNullableGetOnlyProperty(null);

        UnitOfWork.Save(entity);
        UnitOfWork.Clear();

        entity = UnitOfWork.LoadById<EntityWithNullableGetOnlyProperty>(entity.Id);

        entity.Text.ShouldBe(null);
    }
}
#endif