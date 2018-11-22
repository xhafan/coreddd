using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
#if NET40 || NET45
using NHibernate.Type;
#endif

namespace CoreDdd.Nhibernate.Conventions
{
    internal class PreserveMilisecondsDateTimeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(DateTime) || x.Type == typeof(DateTime?));
        }

        public void Apply(IPropertyInstance instance)
        {
#if NET40 || NET45
            instance.CustomType<TimestampType>();
#endif
        }
    }
}