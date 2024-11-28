using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middlewares;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.AspNetCore.Tests.Middlewares.UnitOfWorkMiddlewares.UnitOfWorkMiddlewares
{
    [TestFixture]
    public class when_invoking_for_request_which_should_not_create_transaction
    {
        private UnitOfWorkMiddleware _middleware;
        private bool _nextRequestDelegateIsInvoked;

        [SetUp]
        public void Context()
        {
            _nextRequestDelegateIsInvoked = false;
            _middleware = new UnitOfWorkMiddleware(
                unitOfWorkFactory: new FakeUnitOfWorkFactory(),
                getOrHeadRequestPathsWithoutTransaction:
                [
                    new Regex("/*.js")
                ]
            );
        }

        [TestCase(WebRequestMethods.Http.Get)]
        [TestCase(WebRequestMethods.Http.Head)]
        public async Task next_request_delegate_is_invoked_even_with_unit_of_work_missing(string requestMethod)
        {
            var httpContext = new DefaultHttpContext();
            var httpRequest = httpContext.Request;
            httpRequest.Method = requestMethod;
            httpRequest.Path = new PathString("/js/main.js");

            await _middleware.InvokeAsync(
                httpContext,
                next: _ =>
                {
                    _nextRequestDelegateIsInvoked = true;
                    return Task.CompletedTask;
                }
            );

            _nextRequestDelegateIsInvoked.ShouldBe(true);
        }

        private class FakeUnitOfWorkFactory : IUnitOfWorkFactory
        {
            public IUnitOfWork Create()
            {
                return null;
            }

            public void Release(IUnitOfWork unitOfWork)
            {
            }
        }
    }
}