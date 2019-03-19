using CoreDdd.Nhibernate.TestHelpers;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Components
{
    [TestFixture]
    public class when_persisting_entity_with_component : BasePersistenceTest
    {
        [Test]
        public void entity_can_be_persisted_and_retrieved()
        {
            var entityWithComponent = new EntityWithComponent(23, "some text");

            UnitOfWork.Save(entityWithComponent);
            UnitOfWork.Clear();

            entityWithComponent = UnitOfWork.Get<EntityWithComponent>(entityWithComponent.Id);

            entityWithComponent.MyComponent.Number.ShouldBe(23);
            entityWithComponent.MyComponent.Text.ShouldBe("some text");
            entityWithComponent.MyComponent.AnotherComponentOne.Number.ShouldBe(24);
            entityWithComponent.MyComponent.AnotherComponentOne.Text.ShouldBe("some text 1");
            entityWithComponent.MyComponent.AnotherComponentTwo.Number.ShouldBe(25);
            entityWithComponent.MyComponent.AnotherComponentTwo.Text.ShouldBe("some text 2");
        }
    }
}