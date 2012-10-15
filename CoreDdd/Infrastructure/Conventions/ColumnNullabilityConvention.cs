using System;
using CoreDdd.Domain;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Infrastructure.Conventions
{
    public class ColumnNullabilityConvention :
        IPropertyConvention, IPropertyConventionAcceptance,
        IReferenceConvention, IReferenceConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Nullable, Is.Not.Set);
            criteria.Expect(x => !x.Type.GetUnderlyingSystemType().IsSubclassOfRawGeneric(typeof(Nullable<>)));
        }

        public void Accept(IAcceptanceCriteria<IManyToOneInspector> criteria)
        {
            criteria.Expect(x => x.Nullable, Is.Not.Set);
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Not.Nullable();
        }
    }
}