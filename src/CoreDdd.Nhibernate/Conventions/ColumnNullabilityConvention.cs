#if NET6_0_OR_GREATER
#nullable enable
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
#if NET6_0_OR_GREATER
                var isNullableReferenceType = _determineIsNullableReferenceType();

                bool _determineIsNullableReferenceType() // inspired by https://stackoverflow.com/a/68757807/379279
                {
                    var propertyInfo = x.EntityType.GetProperty(x.Name);
                    Guard.Hope(propertyInfo != null, $"Cannot get property info for {x.EntityType.Name}.{x.Name}");
                    var propertyNullabilityInfo = new NullabilityInfoContext().Create(propertyInfo);
                    return propertyNullabilityInfo.WriteState is NullabilityState.Nullable;
                }
#endif

                return !x.Type.GetUnderlyingSystemType().IsSubclassOfRawGeneric(typeof(Nullable<>))
#if NET6_0_OR_GREATER
                       && !isNullableReferenceType
#endif
                    ;
            });
        }

        public void Accept(IAcceptanceCriteria<IManyToOneInspector> criteria)
        {
            criteria.Expect(x => x.Nullable, Is.Not.Set);
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