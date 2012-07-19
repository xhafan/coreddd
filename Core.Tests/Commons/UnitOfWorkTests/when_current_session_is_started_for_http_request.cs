using System.IO;
using System.Web;
using Core.Infrastructure;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace Core.Tests.Commons.UnitOfWorkTests
{
    [TestFixture]
    public class when_current_session_is_started_for_http_request
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

            UnitOfWork.Current = new UnitOfWork(MockRepository.GenerateStub<ISession>());
            _isStarted = UnitOfWork.IsStarted;
        }

        [Test]
        public void session_is_correct()
        {
            _isStarted.ShouldBe(true);
        }
    }
}