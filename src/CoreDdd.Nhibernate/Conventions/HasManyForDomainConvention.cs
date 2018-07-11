using System;
using System.Collections.Generic;
using System.Linq;
using CoreUtils.Extensions;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    public class HasManyForDomainConvention : IHasManyConvention
    {
        private static Func<string, string> _propertyNameToBackingFieldNameFunc;

        public static void SetPropertyNameToBackingFieldNameFunc(Func<string, string> propertyNameToBackingFieldNameFunc)
        {
            _propertyNameToBackingFieldNameFunc = propertyNameToBackingFieldNameFunc;
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.AllDeleteOrphan();

            var propertyName = instance.Member.Name;
            var parentType = instance.Member.DeclaringType;
            var parentCollectionProperty = parentType.GetInstanceProperties().FirstOrDefault(x => x.Name == propertyName);
            if (parentCollectionProperty == null) return;

            var parentCollectionPropertyBackingFieldName = _propertyNameToBackingFieldNameFunc?.Invoke(propertyName)
                                   ?? "_" + Char.ToLower(propertyName[0]) + propertyName.Substring(1);

            var parentCollectionPropertyBackingField = parentType.GetInstanceFields().FirstOrDefault(x => x.Name == parentCollectionPropertyBackingFieldName);
            if (parentCollectionPropertyBackingField != null)
            {
                instance.Access.ReadOnlyPropertyThroughCamelCaseField(CamelCasePrefix.Underscore);
                _setInverseAndAsSet(parentCollectionPropertyBackingField.PropertyType);
            }
            else
            {
                _setInverseAndAsSet(parentCollectionProperty.PropertyType);
            }

            void _setInverseAndAsSet(Type collectionType)
            {
                if (collectionType.IsSubclassOfRawGeneric(typeof(IList<>))) return;
                if (_doesChildReferenceTheParent())
                {
                    instance.Inverse();
                }
                instance.AsSet();
            }

            bool _doesChildReferenceTheParent()
            {
#if NET40
                var childType = parentCollectionProperty.PropertyType.GetGenericArguments()[0];
#else
                var childType = parentCollectionProperty.PropertyType.GenericTypeArguments[0];
#endif
                var parentPropertyInChildType = childType.GetInstanceProperties().FirstOrDefault(x => x.PropertyType == parentType);
                return parentPropertyInChildType != null;
            }
        }
    }
}
