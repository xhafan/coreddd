using System.IO;
using System.Web;
using Core.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace Core.Tests.Commons.UnitOfWorkTests
{
    [TestFixture]
    public class when_current_session_is_not_started_for_http_request
    {
        private bool _isStarted;

        [SetUp]
        public void Context()
        {
            var httpRequest = new HttpRequest("", "http://url", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContextFake = new HttpContext(httpRequest, httpResponce);
            HttpContext.Current = httpContextFake;

            _isStarted = UnitOfWork.IsStarted;
        }

        [Test]
        public void session_is_correct()
        {
            _isStarted.ShouldBe(false);
        }
    }
}