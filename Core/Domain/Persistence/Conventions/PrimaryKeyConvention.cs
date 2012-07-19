using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Core.Domain.Persistence.Conventions
{
    public class PrimaryKeyConvention : IIdConvention, IIdConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IIdentityInspector> criteria)
        {
            criteria.Expect(x => IsSubclassOfRawGeneric(x));
        }

        private static bool IsSubclassOfRawGeneric(IIdentityInspector x)
        {
            return x.EntityType.IsSubclassOfRawGeneric(typeof (Identity<>));
        }

        public void Apply(IIdentityInstance instance)
        {
            instance.Column("Id");
            instance.GeneratedBy.HiLo("100");
        }
    }
}