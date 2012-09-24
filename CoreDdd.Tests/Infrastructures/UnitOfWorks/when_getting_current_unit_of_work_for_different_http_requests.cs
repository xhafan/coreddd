using System.IO;
using System.Web;
using CoreDdd.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Infrastructures.UnitOfWorks
{
    [TestFixture]
    public class when_getting_current_unit_of_work_for_different_http_requests : base_when_getting_current_unit_of_work
    {
        private UnitOfWork _httpRequestOneUnitOfWork;
        private UnitOfWork _httpRequestTwoUnitOfWork;

        protected override void Context()
        {
            var httpRequest = new HttpRequest("", "http://url", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            
            var httpContextFakeOne = new HttpContext(httpRequest, httpResponce);
            HttpContext.Current = httpContextFakeOne;

            _httpRequestOneUnitOfWork = UnitOfWork.Current;

            var httpContextFakeTwo = new HttpContext(httpRequest, httpResponce);
            HttpContext.Current = httpContextFakeTwo;
            
            _httpRequestTwoUnitOfWork = UnitOfWork.Current;
        }

        [Test]
        public void unit_of_works_are_different()
        {
            _httpRequestOneUnitOfWork.ShouldNotBe(_httpRequestTwoUnitOfWork);
        }
    }
}