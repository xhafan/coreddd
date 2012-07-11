using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Core.Domain.Persistence.Conventions
{
    // http://stackoverflow.com/questions/4852619/how-do-i-map-a-collection-accessed-through-a-read-only-property
    public class HasManyForDomainConvention : IHasManyConvention//, IHasManyConventionAcceptance
    {
//        public void Accept(IAcceptanceCriteria<IOneToManyCollectionInspector> criteria)
//        {
//            criteria.Expect(x => x.EntityType.IsSubclassOfRawGeneric(typeof(Identity<>)));
//        }

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
            }
        }
    }
}
