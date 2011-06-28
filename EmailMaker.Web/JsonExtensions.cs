using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EmailMaker.Web
{
    public static class JsonExtensions
    {
        public static string ToJson(this HtmlHelper htmlHelper, object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
