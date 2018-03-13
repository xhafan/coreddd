using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    public class DisableLazyLoadForDtosConvention : IClassConvention, IClassConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IClassInspector> criteria)
        {
            criteria.Expect(x => x.EntityType.Name.EndsWith("Dto")); // todo: configure how to recognize Dtos in the app
        }

        public void Apply(IClassInstance instance)
        {
            instance.Not.LazyLoad();
        }
    }
}