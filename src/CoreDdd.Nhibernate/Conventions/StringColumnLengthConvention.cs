using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    /// <summary>
    /// Allow to store unlimited string length into string database fields.
    /// </summary>
    public class StringColumnLengthConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
#pragma warning disable 1591
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Type == typeof (string));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Length(10000); // needed for sql server (tested with versions 2005, 2008, 2014)
        }
#pragma warning restore 1591
    }
}