using System;

namespace Core.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IgnoreAutoMapAttribute : Attribute
    {
    }
}