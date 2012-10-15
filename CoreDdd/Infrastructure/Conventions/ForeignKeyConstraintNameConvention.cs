using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Infrastructure.Conventions
{
    public class ForeignKeyConstraintNameConvention : IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.ForeignKey(string.Format("FK_{0}_{1}", instance.EntityType.Name, instance.Property.PropertyType.Name));
        }
    }
}