using System;

namespace Core.Domain.Persistence
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IgnoreAutoMapAttribute : Attribute
    {
    }
}