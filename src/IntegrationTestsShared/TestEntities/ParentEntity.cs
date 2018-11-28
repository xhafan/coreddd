using System.Collections.Generic;
using CoreDdd.Domain;

namespace IntegrationTestsShared.TestEntities
{
    public class ParentEntity : Entity, IAggregateRoot
    {
        private readonly ICollection<ChildEntityReferencingParentEntity> _childrenReferencingParent = new HashSet<ChildEntityReferencingParentEntity>();
        private readonly ICollection<ChildEntityNotReferencingParentEntity> _childrenNotReferencingParent = new HashSet<ChildEntityNotReferencingParentEntity>();

        public virtual IEnumerable<ChildEntityReferencingParentEntity> ChildrenReferencingParent => _childrenReferencingParent;
        public virtual IEnumerable<ChildEntityNotReferencingParentEntity> ChildrenNotReferencingParent => _childrenNotReferencingParent;

        public virtual void AddChildEntityReferencingParentEntity()
        {
            _childrenReferencingParent.Add(new ChildEntityReferencingParentEntity(this));
        }

        public virtual void AddChildEntityNotReferencingParentEntity()
        {
            _childrenNotReferencingParent.Add(new ChildEntityNotReferencingParentEntity());
        }
    }

    public class ChildEntityReferencingParentEntity : Entity
    {
        protected ChildEntityReferencingParentEntity() {}

        public ChildEntityReferencingParentEntity(ParentEntity parentEntity)
        {
            ParentEntity = parentEntity;
        }

        public virtual ParentEntity ParentEntity { get; protected set; }
    }

    public class ChildEntityNotReferencingParentEntity : Entity
    {
    }
}