using System;

namespace Core.Domain
{
    public static class TypeExtensions
    {
        // http://stackoverflow.com/questions/457676/c-sharp-reflection-check-if-a-class-is-derived-from-a-generic-class
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }        
    }
}