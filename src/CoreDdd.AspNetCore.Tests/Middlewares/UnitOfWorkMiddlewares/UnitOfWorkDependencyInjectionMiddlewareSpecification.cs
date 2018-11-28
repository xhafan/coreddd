using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middlewares;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Tests.Middlewares.UnitOfWorkMiddlewares
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
