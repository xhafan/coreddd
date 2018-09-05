#if NETCOREAPP
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middleware;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetCoreTests.UnitOfWorkMiddlewares
{
    public class UnitOfWorkDependencyInjectionMiddlewareSpecification 
        : IUnitOfWorkMiddlewareSpecification
    {
        public async Task CreateMiddlewareAndInvokeHandling(RequestDelegate requestDelegate)
        {
            var unitOfWorkMiddleware = new UnitOfWorkDependencyInjectionMiddleware(requestDelegate);

            var httpContext = new DefaultHttpContext();
            var unitOfWork = IoC.Resolve<IUnitOfWork>();

            await unitOfWorkMiddleware.InvokeAsync(httpContext, unitOfWork);
        }
    }
}
#endif