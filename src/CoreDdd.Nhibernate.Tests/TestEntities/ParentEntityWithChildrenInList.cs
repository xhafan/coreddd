using System.Collections.Generic;
using CoreDdd.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CoreDdd.Nhibernate.Tests.TestEntities
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

    public class ParentEntityWithChildrenInListMappingOverrides : IAutoMappingOverride<ParentEntityWithChildrenInList>
    {
        public void Override(AutoMapping<ParentEntityWithChildrenInList> mapping)
        {
            mapping.HasMany(x => x.ChildrenReferencingParent).AsList(x => x.Column("Index"));
            mapping.HasMany(x => x.ChildrenNotReferencingParent).AsList(x => x.Column("Index"));
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