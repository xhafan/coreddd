#if NET40 || NET45
using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using NHibernate.Type;

namespace CoreDdd.Nhibernate.Conventions
{
    /// <summary>
    /// Preserve miliseconds in the database datetime fields.
    /// </summary>
    public class PreserveMilisecondsDateTimeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
#pragma warning disable 1591
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof(DateTime) || x.Type == typeof(DateTime?));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType<TimestampType>();
        }
#pragma warning restore 1591
    }
}
#endif
