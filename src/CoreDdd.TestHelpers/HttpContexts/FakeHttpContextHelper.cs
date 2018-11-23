#if NETFRAMEWORK
using System.IO;
using System.Web;

namespace CoreDdd.TestHelpers.HttpContexts
{
    /// <summary>
    /// Fake HttpContext helper to enable unit testing of a code using HttpContext.Current.
    /// </summary>
    public static class FakeHttpContextHelper
    {
        /// <summary>
        /// Gets faked instanced of HttpContext class.
        /// </summary>
        /// <returns></returns>
        public static HttpContext GetFakeHttpContext()
        {
            // inspired by https://stackoverflow.com/a/10126711/379279

            var httpRequest = new HttpRequest("", "http://localhost", "");
            var httpResponse = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(httpRequest, httpResponse);
            return httpContext;
        }
    }
}
#endif