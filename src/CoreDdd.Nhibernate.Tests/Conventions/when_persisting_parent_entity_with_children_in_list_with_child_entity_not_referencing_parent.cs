using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreUtils.Extensions;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Conventions
{
    [TestFixture]
    public class when_persisting_parent_entity_with_children_in_list_with_child_entity_not_referencing_parent 
        : BasePersistenceTest
    {
        private ParentEntityWithChildrenInList _parent;

        [SetUp]
        public void Context()
        {
            _parent = new ParentEntityWithChildrenInList();
            _parent.AddChildEntityNotReferencingParentEntity();
            _parent.AddChildEntityNotReferencingParentEntity();

            UnitOfWork.Save(_parent);
            UnitOfWork.Clear();

            _parent = UnitOfWork.Get<ParentEntityWithChildrenInList>(_parent.Id);
        }

        [Test]
        public void child_entity_is_persisted_linked_with_the_parent()
        {
            _parent.ChildrenNotReferencingParent.Count().ShouldBe(2);
        }

        [Test]
        public void child_entity_index_is_set_correctly()
        {
            _parent.ChildrenNotReferencingParent.First().Index.ShouldBe(0);
            _parent.ChildrenNotReferencingParent.Second().Index.ShouldBe(1);
        }
    }
}