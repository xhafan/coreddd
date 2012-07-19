using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Tests.Helpers.Extensions
{
    public static class ObjectExtensions
    {
        public static void SetPrivateAttribute(this object obj, string attributeName, object value)
        {
            obj.GetType().GetField(attributeName, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(obj, value);
        }

        public static void SetPrivateProperty(this object obj, string propertyName, object value)
        {
            obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).SetValue(obj, value, null);
        }

        public static void SetPrivateProperty<T>(this T target, Expression<Func<T, object>> property, object propertyValue)
        {
            SetPrivateProperty(target, GetPropertyName(property), propertyValue);
        }

        // http://beta.blogs.microsoft.co.il/blogs/smallfish/archive/2009/04/02/how-to-get-a-property-name-using-lambda-expression-in-c-3-0.aspx
        public static string GetPropertyName<T>(Expression<Func<T, object>> property)
        {
            return GetMemberName(property.Body);
        }

        public static string GetMemberName(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression.Expression)
                           + "."
                           + memberExpression.Member.Name;
                }
                return memberExpression.Member.Name;
            }
            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
            {
                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format(
                        "Cannot interpret member from {0}",
                        expression));

                return GetMemberName(unaryExpression.Operand);
            }
            throw new Exception(string.Format("Could not determine member from {0}", expression));
        }
        
//        public static object GetPrivateAttribute(this object obj, string attributeName)
//        {
//            return obj.GetType().GetField(attributeName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
//        }
    }
}
