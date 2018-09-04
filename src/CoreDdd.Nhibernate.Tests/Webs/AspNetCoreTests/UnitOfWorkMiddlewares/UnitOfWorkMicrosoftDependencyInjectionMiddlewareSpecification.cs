#if NETCOREAPP
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middleware;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetCoreTests.UnitOfWorkMiddlewares
{
    public class UnitOfWorkMicrosoftDependencyInjectionMiddlewareSpecification 
        : IUnitOfWorkMiddlewareSpecification
    {
        public async Task CreateMiddlewareAndInvokeHandling(RequestDelegate requestDelegate)
        {
            var unitOfWorkMiddleware = new UnitOfWorkMicrosoftDependencyInjectionMiddleware(requestDelegate);

            var httpContext = new DefaultHttpContext();
            var unitOfWork = IoC.Resolve<IUnitOfWork>();

            await unitOfWorkMiddleware.InvokeAsync(httpContext, unitOfWork);
        }
    }
}
#endif