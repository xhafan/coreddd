using CoreDdd.Nhibernate.TestHelpers;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;
using System.Linq;

namespace CoreDdd.Nhibernate.Tests.Conventions
{
    [TestFixture]
    public class when_persisting_parent_entity_with_children_in_dictionary_with_child_entity_referencing_parent 
        : BasePersistenceTest
    {
        private ParentEntityWithChildrenInDictionary _parent;

        [SetUp]
        public void Context()
        {
            _parent = new ParentEntityWithChildrenInDictionary();
            _parent.AddChildEntityReferencingParentEntity(23);
            _parent.AddChildEntityReferencingParentEntity(24);

            UnitOfWork.Save(_parent);
            UnitOfWork.Clear();

            _parent = UnitOfWork.Get<ParentEntityWithChildrenInDictionary>(_parent.Id);
        }

        [Test]
        public void child_entity_is_persisted_linked_with_the_parent()
        {
            _parent.ChildrenReferencingParent.Count.ShouldBe(2);
        }

        [Test]
        public void dictionary_collection_is_correct()
        {
            _parent.ChildrenReferencingParent.Keys.ShouldBe(new[] {23, 24}, ignoreOrder: true);
            _parent.ChildrenReferencingParent.Select(x => x.Key).ShouldBe(new[] {23, 24}, ignoreOrder: true);
        }
    }
}