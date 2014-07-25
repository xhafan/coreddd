using System;

namespace CoreDdd.Extensions
{
    public static class TypeExtensions
    {
        // http://stackoverflow.com/questions/457676/c-sharp-reflection-check-if-a-class-is-derived-from-a-generic-class
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic) // todo: move this to CoreUtils?
        {
            while (toCheck != typeof(object) && toCheck != null)
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