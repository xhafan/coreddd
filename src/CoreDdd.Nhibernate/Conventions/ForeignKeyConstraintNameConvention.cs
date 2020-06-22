using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    /// <summary>
    /// Defines foreign key constraint name.
    /// </summary>
    public class ForeignKeyConstraintNameConvention : IReferenceConvention
    {
#pragma warning disable 1591
        public void Apply(IManyToOneInstance instance)
#pragma warning restore 1591
        {
            instance.ForeignKey($"FK_{instance.EntityType.Name}_{instance.Property.PropertyType.Name}");
        }
    }
}