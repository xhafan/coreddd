using System.Collections.Generic;
using CoreDdd.Domain;

namespace IntegrationTestsShared.TestEntities
{
    public class ParentEntityWithChildrenInList : Entity, IAggregateRoot
    {
        private readonly IList<ChildEntityInListReferencingParentEntity> _childrenReferencingParent = 
            new List<ChildEntityInListReferencingParentEntity>();
        private readonly IList<ChildEntityInListNotReferencingParentEntity> _childrenNotReferencingParent = 
            new List<ChildEntityInListNotReferencingParentEntity>();

        public virtual IEnumerable<ChildEntityInListReferencingParentEntity> ChildrenReferencingParent => _childrenReferencingParent;
        public virtual IEnumerable<ChildEntityInListNotReferencingParentEntity> ChildrenNotReferencingParent => _childrenNotReferencingParent;

        public virtual void AddChildEntityReferencingParentEntity()
        {
            _childrenReferencingParent.Add(new ChildEntityInListReferencingParentEntity(this));
        }

        public virtual void AddChildEntityNotReferencingParentEntity()
        {
            _childrenNotReferencingParent.Add(new ChildEntityInListNotReferencingParentEntity());
        }
    }

    public class ChildEntityInListReferencingParentEntity : Entity
    {
        protected ChildEntityInListReferencingParentEntity() { }

        public ChildEntityInListReferencingParentEntity(ParentEntityWithChildrenInList parent)
        {
            Parent = parent;
        }

        public virtual ParentEntityWithChildrenInList Parent { get; protected set; }
        public virtual int Index { get; protected set; }
    }

    public class ChildEntityInListNotReferencingParentEntity : Entity
    {
        public virtual int Index { get; protected set; }
    }
}