using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace CoreDdd.Nhibernate.Conventions
{
    public class CustomForeignKeyConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            return type.Name + "Id";
        }
    }
}