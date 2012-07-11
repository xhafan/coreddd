using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Core.Domain.Persistence.Conventions
{
    public class JoinedSubclassIdConvention : IJoinedSubclassConvention
    {
        public void Apply(IJoinedSubclassInstance instance)
        {
            //instance.Key.Column("Id");
        }
    }
}