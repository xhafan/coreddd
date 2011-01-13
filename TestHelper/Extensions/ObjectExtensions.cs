using System.Reflection;

namespace TestHelper.Extensions
{
    public static class ObjectExtensions
    {
        public static void SetPrivateAttribute(this object obj, string attributeName, object value)
        {
            obj.GetType().GetField(attributeName, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(obj, value);
        }
    }
}
