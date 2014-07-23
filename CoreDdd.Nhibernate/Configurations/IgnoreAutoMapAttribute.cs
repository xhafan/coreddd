using System;

namespace CoreDdd.Nhibernate.Configurations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IgnoreAutoMapAttribute : Attribute
    {
    }
}