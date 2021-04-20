using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    /// <summary>
    /// Load DTOs as ReadOnly - don't track changes
    /// </summary>
    public class LoadDtosAsReadonlyConvention : IClassConvention, IClassConventionAcceptance
    {
        private static Func<Type, bool> _isTypeDto;

#pragma warning disable 1591
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
            instance.ReadOnly();
        }
#pragma warning restore 1591
    }
}