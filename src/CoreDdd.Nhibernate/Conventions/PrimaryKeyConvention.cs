using CoreDdd.Domain;
using CoreUtils;
using CoreUtils.Extensions;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace CoreDdd.Nhibernate.Conventions;

/// <summary>
/// Primary key default HiLo convention.
/// </summary>
public class PrimaryKeyConvention : IIdConvention, IIdConventionAcceptance
{
    private static string? _maxLo;

#pragma warning disable 1591
    public static void Initialize(string maxLo)
    {
        _maxLo = maxLo;
    }

    public void Accept(IAcceptanceCriteria<IIdentityInspector> criteria)
    {
        criteria.Expect(x => x.EntityType.IsSubclassOfRawGeneric(typeof (Entity<>)));
    }

    public void Apply(IIdentityInstance instance)
    {
        Guard.Hope(_maxLo != null, "PrimaryKeyConvention not initialized. Call PrimaryKeyConvention.Initialize() before using it.");

        if (instance.Type == typeof(int) 
            || instance.Type == typeof(long))
        {
            instance.Column("Id");
            instance.GeneratedBy.HiLo(_maxLo);
        }
    }
#pragma warning restore 1591
}