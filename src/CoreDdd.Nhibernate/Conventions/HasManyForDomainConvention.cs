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
    // todo: implement conventions as opt-in
    public class HasManyForDomainConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.AllDeleteOrphan();

            var propertyName = instance.Member.Name;
            var declaringType = instance.Member.DeclaringType;
            var property = declaringType.GetInstanceProperties().FirstOrDefault(x => x.Name == propertyName);
            if (property == null) return;
            var backingFieldName = "_" + Char.ToLower(propertyName[0]) + propertyName.Substring(1); // todo: backing field name - configure it in the app?
            var field = declaringType.GetInstanceFields().FirstOrDefault(x => x.Name == backingFieldName);
            if (field != null)
            {
                instance.Access.ReadOnlyPropertyThroughCamelCaseField(CamelCasePrefix.Underscore);
                SetInverseAndAsSetIfNotAList(field.PropertyType);
            }
            else
            {
                SetInverseAndAsSetIfNotAList(property.PropertyType);
            }

            void SetInverseAndAsSetIfNotAList(Type collectionType)
            {
                if (collectionType.IsSubclassOfRawGeneric(typeof(IList<>))) return;
                instance.Inverse(); // inverse by default if not an ordered list
                instance.AsSet();
            }
        }
    }
}
