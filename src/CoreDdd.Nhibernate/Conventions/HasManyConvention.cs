using System;
using System.Collections.Generic;
using System.Linq;
using CoreUtils;
using CoreUtils.Extensions;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    /// <summary>
    /// Default 'has many' relationship convention.
    /// Allows to specify cascade strategy and backing field access strategy.
    /// A collection is treated as set by default with one exception - IList collection is treated as list.
    /// </summary>
    public class HasManyConvention : IHasManyConvention
    {
        private static Action<ICollectionCascadeInstance>? _collectionCascadeInstanceAction;
        private static Func<string, string>? _getBackingFieldNameFromPropertyName;
        private static Action<IAccessInstance>? _setCollectionInstanceAccess;

#pragma warning disable 1591
        public static void Initialize(
            Action<ICollectionCascadeInstance> collectionCascadeInstanceAction,
            Func<string, string> getBackingFieldNameFromPropertyName,
            Action<IAccessInstance> setCollectionInstanceAccess
            )
        {
            _collectionCascadeInstanceAction = collectionCascadeInstanceAction;
            _getBackingFieldNameFromPropertyName = getBackingFieldNameFromPropertyName;
            _setCollectionInstanceAccess = setCollectionInstanceAccess;
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            Guard.Hope(_collectionCascadeInstanceAction != null, nameof(_collectionCascadeInstanceAction) + " is null. Call Initialize() first.");
            Guard.Hope(_getBackingFieldNameFromPropertyName != null, nameof(_getBackingFieldNameFromPropertyName) + " is null. Call Initialize() first.");
            Guard.Hope(_setCollectionInstanceAccess != null, nameof(_setCollectionInstanceAccess) + " is null. Call Initialize() first.");
            
            _collectionCascadeInstanceAction!.Invoke(instance.Cascade);

            var propertyName = instance.Member.Name;
            var parentType = instance.Member.DeclaringType;
            var parentCollectionProperty = parentType.GetInstanceProperties().FirstOrDefault(x => x.Name == propertyName);
            if (parentCollectionProperty == null) return;

            
            var parentCollectionPropertyBackingFieldName = _getBackingFieldNameFromPropertyName!(propertyName);

            var parentCollectionPropertyBackingField = parentType.GetInstanceFields().FirstOrDefault(x => x.Name == parentCollectionPropertyBackingFieldName);
            if (parentCollectionPropertyBackingField != null)
            {
                _setCollectionInstanceAccess!(instance.Access);
                _markAsInverseIfPossibleAndSetAsSetOrAsListOrMap(parentCollectionPropertyBackingField.PropertyType);
            }
            else
            {
                _markAsInverseIfPossibleAndSetAsSetOrAsListOrMap(parentCollectionProperty.PropertyType);
            }

            void _markAsInverseIfPossibleAndSetAsSetOrAsListOrMap(Type collectionType)
            {
                if (collectionType.IsSubclassOfRawGeneric(typeof(IList<>)))
                {
                    instance.AsList();
                    return;
                }
                if (_doesChildReferenceTheParent())
                {
                    instance.Inverse();
                }
                if (collectionType.IsSubclassOfRawGeneric(typeof(IDictionary<,>)))
                {
                    instance.AsMap();
                    return;
                }
                instance.AsSet();
            }

            bool _doesChildReferenceTheParent()
            {
#if NET40
                var childTypes = parentCollectionProperty.PropertyType.GetGenericArguments();
#else
                var childTypes = parentCollectionProperty.PropertyType.GenericTypeArguments;
#endif
                var parentPropertyInChildType = childTypes.SelectMany(x => x.GetInstanceProperties()).FirstOrDefault(x => x.PropertyType == parentType);
                return parentPropertyInChildType != null;
            }
        }
#pragma warning restore 1591
    }
}
