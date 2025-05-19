using System;
using CoreUtils;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

#if !NET8_0_OR_GREATER
#nullable disable
#endif

namespace CoreDdd.Nhibernate.Conventions
{
    /// <summary>
    /// Disable lazy loading for DTO types.
    /// </summary>
    public class DisableLazyLoadForDtosConvention : IClassConvention, IClassConventionAcceptance
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        private static Func<Type, bool>? _isTypeDto;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

#pragma warning disable 1591
        public static void Initialize(Func<Type, bool> isTypeDto)
        {
            _isTypeDto = isTypeDto;
        }

        public void Accept(IAcceptanceCriteria<IClassInspector> criteria)
        {
            criteria.Expect(x =>
            {
                Guard.Hope(_isTypeDto != null, nameof(_isTypeDto) + " is null. Call Initialize() first.");
                return _isTypeDto(x.EntityType);
            });
        }

        public void Apply(IClassInstance instance)
        {
            instance.Not.LazyLoad();
        }
#pragma warning restore 1591
    }
}
