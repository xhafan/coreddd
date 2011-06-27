using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EmailMaker.Web
{
    public static class JsonExtensions
    {
        public static string ToJson(this HtmlHelper htmlHelper, object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static string GetSerializedJSON(this HtmlHelper htmlHelper, object obj)
        {
            return new JavaScriptSerializer().Serialize(new { data = obj });
        }

        public static string GetSerializedJSONWithNoParent(this HtmlHelper htmlHelper, object obj)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            var sw = new StringWriter();

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, obj);
            }

            return sw.ToString();

        }
    }
}
