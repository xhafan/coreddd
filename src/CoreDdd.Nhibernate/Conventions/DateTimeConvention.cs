using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using NHibernate.Type;

namespace CoreDdd.Nhibernate.Conventions
{
    // preserves miliseconds in the db
    public class DateTimeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(DateTime) || x.Type == typeof(DateTime?));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType<TimestampType>();
        }
    }
}