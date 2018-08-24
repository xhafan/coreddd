#if NETFRAMEWORK
using System.IO;
using System.Web;

namespace CoreDdd.TestHelpers.HttpContexts
{
    public static class FakeHttpContextHelper
    {
        // inspired by https://stackoverflow.com/a/10126711/379279
        public static HttpContext GetFakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);
            return httpContext;
        }
    }
}
#endif