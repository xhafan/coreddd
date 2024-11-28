using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.AspNetCore.Tests.Middlewares.UnitOfWorkMiddlewares.UnitOfWorkDependencyInjectionMiddlewares
{
    [TestFixture]
    public class when_invoking_for_request_which_should_not_create_transaction
    {
        private UnitOfWorkDependencyInjectionMiddleware _middleware;
        private bool _nextRequestDelegateIsInvoked;

        [SetUp]
        public void Context()
        {
            _nextRequestDelegateIsInvoked = false;
            _middleware = new UnitOfWorkDependencyInjectionMiddleware(
                next: _ =>
                {
                    _nextRequestDelegateIsInvoked = true;
                    return Task.CompletedTask;
                },
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
                unitOfWork: null
            );

            _nextRequestDelegateIsInvoked.ShouldBe(true);
        }
    }
}