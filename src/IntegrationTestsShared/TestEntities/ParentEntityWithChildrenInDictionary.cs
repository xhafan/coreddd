using System.Collections.Generic;
using CoreDdd.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace IntegrationTestsShared.TestEntities
{
    public class ParentEntityWithChildrenInDictionary : Entity, IAggregateRoot
    {
        private readonly IDictionary<int, ChildEntityInDictionaryReferencingParentEntity> _childrenReferencingParent = 
            new Dictionary<int, ChildEntityInDictionaryReferencingParentEntity>();
        private readonly IDictionary<int, ChildEntityInDictionaryNotReferencingParentEntity> _childrenNotReferencingParent = 
            new Dictionary<int, ChildEntityInDictionaryNotReferencingParentEntity>();

        public virtual IDictionary<int, ChildEntityInDictionaryReferencingParentEntity> ChildrenReferencingParent => _childrenReferencingParent;
        public virtual IDictionary<int, ChildEntityInDictionaryNotReferencingParentEntity> ChildrenNotReferencingParent => _childrenNotReferencingParent;

        public virtual void AddChildEntityReferencingParentEntity(int key)
        {
            _childrenReferencingParent.Add(key, new ChildEntityInDictionaryReferencingParentEntity(this, key));
        }

        public virtual void AddChildEntityNotReferencingParentEntity(int key)
        {
            _childrenNotReferencingParent.Add(key, new ChildEntityInDictionaryNotReferencingParentEntity(key));
        }
    }

    public class ChildEntityInDictionaryReferencingParentEntity : Entity
    {
        protected ChildEntityInDictionaryReferencingParentEntity() { }

        public ChildEntityInDictionaryReferencingParentEntity(ParentEntityWithChildrenInDictionary parent, int key)
        {
            Parent = parent;
            Key = key;
        }

        public virtual ParentEntityWithChildrenInDictionary Parent { get; protected set; }
        public virtual int Key { get; protected set; }
    }

    public class ChildEntityInDictionaryNotReferencingParentEntity : Entity
    {
        protected ChildEntityInDictionaryNotReferencingParentEntity() { }

        public ChildEntityInDictionaryNotReferencingParentEntity(int key)
        {
            Key = key;
        }

        public virtual int Key { get; protected set; }
    }

    public class ParentEntityWithChildrenInDictionaryMappingOverrides : IAutoMappingOverride<ParentEntityWithChildrenInDictionary>
    {
        public void Override(AutoMapping<ParentEntityWithChildrenInDictionary> mapping)
        {
            mapping.HasMany(x => x.ChildrenReferencingParent)
                .AsMap(x => x.Key);

            mapping.HasMany(x => x.ChildrenNotReferencingParent)
                .AsMap(x => x.Key);
        }
    }
}