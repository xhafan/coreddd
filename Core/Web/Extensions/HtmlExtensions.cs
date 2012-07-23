using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Core.Web.Utilities;

namespace Core.Web.Extensions
{
    public static class HtmlExtensions
    {
        public static string BuildUrlFromExpressionWithoutParameters<TController>(this HtmlHelper htmlHelper, Expression<Action<TController>> action) 
            where TController : Controller
        {
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var routeValues = RouteBuilder.GetRouteValuesFromExpression(action, false);
            return url.Content(string.Format("~/{0}/{1}", routeValues["Controller"], routeValues["Action"]));
        }
    }
}
