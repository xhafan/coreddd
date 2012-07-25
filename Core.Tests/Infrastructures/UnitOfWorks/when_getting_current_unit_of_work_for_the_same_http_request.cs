using System.IO;
using System.Web;
using Core.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace Core.Tests.Infrastructures.UnitOfWorks
{
    [TestFixture]
    public class when_getting_current_unit_of_work_for_the_same_http_request : base_when_getting_current_unit_of_work
    {
        private UnitOfWork _currentUnitOfWork;
        private UnitOfWork _anotherCurrentUnitOfWork;

        protected override void Context()
        {
            var httpRequest = new HttpRequest("", "http://url", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContextFake = new HttpContext(httpRequest, httpResponce);

            HttpContext.Current = httpContextFake;

            _currentUnitOfWork = UnitOfWork.Current;
            _anotherCurrentUnitOfWork = UnitOfWork.Current;
        }

        [Test]
        public void unit_of_works_are_the_same()
        {
            _currentUnitOfWork.ShouldBe(_anotherCurrentUnitOfWork);
        }
    }
}