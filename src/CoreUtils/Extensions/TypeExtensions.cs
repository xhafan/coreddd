using System;
#if NET40
#else
using System.Reflection;
#endif

namespace CoreUtils.Extensions
{
    /// <summary>
    /// Type extension methods.
    /// </summary>
    public static class TypeExtensions
    {        
        /// <summary>
        /// Determines if a type is a subclass of a given generic type.
        /// </summary>
        /// <param name="toCheck">A type to check</param>
        /// <param name="generic">A generic type</param>
        /// <returns>true if the type is a subclass of the generic type, returns false otherwise</returns>
        public static bool IsSubclassOfRawGeneric(this Type? toCheck, Type generic)
        {
            // http://stackoverflow.com/questions/457676/c-sharp-reflection-check-if-a-class-is-derived-from-a-generic-class
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