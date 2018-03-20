using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    public class DisableLazyLoadForDtosConvention : IClassConvention, IClassConventionAcceptance
    {
        private static Func<Type, bool> _isTypeDtoFunc;

        public static void SetFuncToDetermineIfTypeIsDto(Func<Type, bool> isTypeDtoFunc)
        {
            _isTypeDtoFunc = isTypeDtoFunc;
        }

        public void Accept(IAcceptanceCriteria<IClassInspector> criteria)
        {
            criteria.Expect(x => _isTypeDtoFunc?.Invoke(x.EntityType) ?? x.EntityType.Name.EndsWith("Dto"));
        }

        public void Apply(IClassInstance instance)
        {
            instance.Not.LazyLoad();
        }
    }
}