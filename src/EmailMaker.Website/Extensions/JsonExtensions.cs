using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EmailMaker.Website.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this HtmlHelper htmlHelper, object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
