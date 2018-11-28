using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middlewares;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Tests.Middlewares.UnitOfWorkMiddlewares
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
