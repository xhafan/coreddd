#if NET8_0_OR_GREATER
using System.Reflection;
using CoreUtils;
#endif

using System;
using CoreUtils.Extensions;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions
{
    /// <summary>
    /// Default DB column nullability convention.
    /// Not nullable property is mapped into a not nullable column.
    /// Nullable property is mapped into a nullable column.
    /// Nullable reference types are supported.
    /// </summary>
    public class ColumnNullabilityConvention :
        IPropertyConvention, IPropertyConventionAcceptance,
        IReferenceConvention, IReferenceConventionAcceptance
    {
#pragma warning disable 1591
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Expect(x => x.Nullable, Is.Not.Set);
            criteria.Expect(x =>
            {
#if NET8_0_OR_GREATER
                var isNullableReferenceType = _DetermineIsNullableReferenceType(x.EntityType, x.Name);
#endif

                return !x.Type.GetUnderlyingSystemType().IsSubclassOfRawGeneric(typeof(Nullable<>))
#if NET8_0_OR_GREATER
                       && !isNullableReferenceType
#endif
                    ;
            });
        }

#if NET8_0_OR_GREATER
        private bool _DetermineIsNullableReferenceType(Type entityType, string propertyName) // inspired by https://stackoverflow.com/a/68757807/379279
        {
            var propertyInfo = entityType.GetProperty(propertyName);
            Guard.Hope(propertyInfo != null, $"Cannot get property info for {entityType.Name}.{propertyName}");
            var propertyNullabilityInfo = new NullabilityInfoContext().Create(propertyInfo);
            return propertyNullabilityInfo.WriteState is NullabilityState.Nullable;
        }
#endif

        public void Accept(IAcceptanceCriteria<IManyToOneInspector> criteria)
        {
            criteria.Expect(x => x.Nullable, Is.Not.Set);
#if NET8_0_OR_GREATER
            criteria.Expect(x =>
            {
                var isNullableReferenceType = _DetermineIsNullableReferenceType(x.EntityType, x.Name);
                return !isNullableReferenceType;
            });
#endif
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.Not.Nullable();
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.Not.Nullable();
        }
#pragma warning restore 1591
    }
}