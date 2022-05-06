using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.AspNetCore.Tests.Middlewares.TransactionScopeUnitOfWorkMiddlewares.TransactionScopeUnitOfWorkDependencyInjectionMiddlewares
{
    [TestFixture]
    public class when_invoking_for_request_which_should_not_create_transaction
    {
        private TransactionScopeUnitOfWorkDependencyInjectionMiddleware _middleware;
        private bool _nextRequestDelegateIsInvoked;

        [SetUp]
        public void Context()
        {
            _nextRequestDelegateIsInvoked = false;
            _middleware = new TransactionScopeUnitOfWorkDependencyInjectionMiddleware(
                next: context =>
                {
                    _nextRequestDelegateIsInvoked = true;
                    return Task.CompletedTask;
                },
                getOrHeadRequestPathsWithoutTransaction: new []
                {
                    new Regex(@"/*.js")
                }
            );
        }

        [TestCase(WebRequestMethods.Http.Get)]
        [TestCase(WebRequestMethods.Http.Head)]
        public async Task next_request_delegate_is_invoked_even_with_unit_of_work_missing(string requestMethod)
        {
            var httpContext = new DefaultHttpContext();
            var httpRequest = (DefaultHttpRequest)httpContext.Request;
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