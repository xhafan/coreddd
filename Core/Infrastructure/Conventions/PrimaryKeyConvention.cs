using Core.Domain;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Core.Infrastructure.Conventions
{
    public class PrimaryKeyConvention : IIdConvention, IIdConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IIdentityInspector> criteria)
        {
            criteria.Expect(x => x.EntityType.IsSubclassOfRawGeneric(typeof (Identity<>)));
        }

        public void Apply(IIdentityInstance instance)
        {
            instance.Column("Id");
            instance.GeneratedBy.HiLo("100");
        }
    }
}