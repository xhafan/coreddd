using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Conventions
{
    [TestFixture]
    public class when_persisting_parent_entity_with_child_entity_not_referencing_parent : BasePersistenceTest
    {
        [Test]
        public void child_entity_is_persisted_linked_with_the_parent()
        {
            var parentEntity = new ParentEntity();
            parentEntity.AddChildEntityNotReferencingParentEntity();

            UnitOfWork.Save(parentEntity);
            UnitOfWork.Clear();

            parentEntity = UnitOfWork.Get<ParentEntity>(parentEntity.Id);

            parentEntity.ChildrenNotReferencingParent.Count().ShouldBe(1);
        }
    }
}