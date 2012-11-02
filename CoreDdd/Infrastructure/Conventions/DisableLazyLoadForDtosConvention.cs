using CoreDdd.Dtos;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Infrastructure.Conventions
{
    public class DisableLazyLoadForDtosConvention : IClassConvention, IClassConventionAcceptance
    {
        public void Accept(IAcceptanceCriteria<IClassInspector> criteria)
        {
            criteria.Expect(x => x.EntityType.IsSubclassOf(typeof(Dto)));
        }

        public void Apply(IClassInstance instance)
        {
            instance.Not.LazyLoad();
        }
    }
}