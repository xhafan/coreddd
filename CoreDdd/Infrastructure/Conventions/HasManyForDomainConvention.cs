using System;
using System.Collections.Generic;
using System.Linq;
using CoreDdd.Domain;
using CoreDdd.Extensions;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Infrastructure.Conventions
{
    public class HasManyForDomainConvention : IHasManyConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.AllDeleteOrphan();

            var propertyName = instance.Member.Name;
            var declaringType = instance.Member.DeclaringType;
            var property = declaringType.GetInstanceProperties().FirstOrDefault(x => x.Name == propertyName);
            if (property == null) return;
            var backingFieldName = "_" + Char.ToLower(propertyName[0]) + propertyName.Substring(1);
            var field = declaringType.GetInstanceFields().FirstOrDefault(x => x.Name == backingFieldName);
            if (field != null)
            {
                instance.Access.ReadOnlyPropertyThroughCamelCaseField(CamelCasePrefix.Underscore);
                if (!field.PropertyType.IsSubclassOfRawGeneric(typeof (IList<>)))
                {
                    instance.Inverse(); // inverse by default if not ordered list
                }
            }
            else
            {
                if (!property.PropertyType.IsSubclassOfRawGeneric(typeof (IList<>)))
                {
                    instance.Inverse(); // inverse by default if not ordered list
                }
            }
        }
    }
}
