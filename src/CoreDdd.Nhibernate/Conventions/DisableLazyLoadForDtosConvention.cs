using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    public class DisableLazyLoadForDtosConvention : IClassConvention, IClassConventionAcceptance
    {
        private static Func<Type, bool> _isTypeDto;

        public static void Initialize(Func<Type, bool> isTypeDto)
        {
            _isTypeDto = isTypeDto;
        }

        public void Accept(IAcceptanceCriteria<IClassInspector> criteria)
        {
            criteria.Expect(x => _isTypeDto(x.EntityType));
        }

        public void Apply(IClassInstance instance)
        {
            instance.Not.LazyLoad();
        }
    }
}