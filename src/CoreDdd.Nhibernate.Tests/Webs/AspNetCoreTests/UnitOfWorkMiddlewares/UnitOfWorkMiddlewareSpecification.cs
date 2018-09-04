#if NETCOREAPP
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middleware;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetCoreTests.UnitOfWorkMiddlewares
{
    public class UnitOfWorkMiddlewareSpecification : IUnitOfWorkMiddlewareSpecification
    {
        public async Task CreateMiddlewareAndInvokeHandling(RequestDelegate requestDelegate)
        {
            var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
            var unitOfWorkMiddleware = new UnitOfWorkMiddleware(unitOfWorkFactory);

            var httpContext = new DefaultHttpContext();

            await unitOfWorkMiddleware.InvokeAsync(httpContext, requestDelegate);
        }
    }
}
#endif