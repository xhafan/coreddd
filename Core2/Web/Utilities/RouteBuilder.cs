using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Utilities;
using Core.Utilities.Extensions;


namespace Core.Web.Utilities
{
    // http://stackoverflow.com/questions/369102/using-a-strongly-typed-actionlink-when-the-action-method-doesnt-take-a-primitive
    public class RouteBuilder
    {
        public static RouteValueDictionary GetRouteValuesFromExpression<TController>(Expression<Action<TController>> action, bool withParameters)
                    where TController : Controller
        {
            Guard.Hope(action != null, @"Action passed to GetRouteValuesFromExpression cannot be null.");
            var methodCall = action.Body as MethodCallExpression;
            Guard.Hope(methodCall != null, @"Action passed to GetRouteValuesFromExpression must be method call");
            var controllerName = typeof(TController).Name;
            Guard.Hope(controllerName.EndsWith("Controller"), @"Controller passed to GetRouteValuesFromExpression is incorrect");

            var rvd = new RouteValueDictionary
                          {
                              {"Controller", controllerName.Substring(0, controllerName.Length - "Controller".Length)},
                              {"Action", methodCall.Method.Name}
                          };
            if (withParameters)
            {
                AddParameterValuesFromExpressionToDictionary(rvd, methodCall);                
            }
            return rvd;
        }

        /// <summary>
        /// Adds a route value for each parameter in the passed in expression.  If the parameter is primitive it just uses its name and value
        /// if not, it creates a route value for each property on the object with the property's name and value.
        /// </summary>
        /// <param name="routeValues"></param>
        /// <param name="methodCall"></param>
        private static void AddParameterValuesFromExpressionToDictionary(RouteValueDictionary routeValues, MethodCallExpression methodCall)
        {
            var parameters = methodCall.Method.GetParameters();
            methodCall.Arguments.Each(argument =>
            {
                var index = methodCall.Arguments.IndexOf(argument);

                var constExpression = argument as ConstantExpression;
                if (constExpression != null)
                {
                    var value = constExpression.Value;
                    routeValues.Add(parameters[index].Name, value);
                }
                else
                {
                    object actualArgument = argument;
                    var expression = argument as MemberInitExpression;
                    if (expression != null)
                    {
                        actualArgument = Expression.Lambda(argument).Compile().DynamicInvoke();
                    }

                    // create a route value for each property on the object
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(actualArgument))
                    {
                        object obj2 = descriptor.GetValue(actualArgument);
                        routeValues.Add(descriptor.Name, obj2);
                    }
                }
            });
        }
    }
}
