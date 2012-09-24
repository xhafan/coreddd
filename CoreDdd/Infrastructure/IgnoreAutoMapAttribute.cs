using System;

namespace CoreDdd.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IgnoreAutoMapAttribute : Attribute
    {
    }
}