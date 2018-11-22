using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    internal class ForeignKeyConstraintNameConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.ForeignKey($"FK_{instance.EntityType.Name}_{instance.Property.PropertyType.Name}");
        }
    }
}