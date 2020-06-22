using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace CoreDdd.Nhibernate.Conventions
{
    /// <summary>
    /// Defines foreign key column name.
    /// </summary>
    public class ForeignKeyNameConvention : ForeignKeyConvention
    {
#pragma warning disable 1591
        protected override string GetKeyName(Member property, Type type)
#pragma warning restore 1591
        {
            return type.Name + "Id";
        }
    }
}