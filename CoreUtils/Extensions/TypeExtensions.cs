using System;
#if NET40
#else
using System.Reflection;
#endif

namespace CoreUtils.Extensions
{
    public static class TypeExtensions
    {
        // http://stackoverflow.com/questions/457676/c-sharp-reflection-check-if-a-class-is-derived-from-a-generic-class
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != typeof(object) && toCheck != null)
            {
#if NET40
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
#else
                var cur = toCheck.GetTypeInfo().IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
#endif
                if (generic == cur)
                {
                    return true;
                }
#if NET40
                toCheck = toCheck.BaseType;
#else
                toCheck = toCheck.GetTypeInfo().BaseType;
#endif
            }
            return false;
        }        
    }
}