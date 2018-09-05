#if NETCOREAPP
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middleware;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetCoreTests.TransactionScopeUnitOfWorkMiddlewares
{
    public class TransactionScopeUnitOfWorkDependencyInjectionMiddlewareSpecification 
        : ITransactionScopeUnitOfWorkMiddlewareSpecification
    {
        public async Task CreateMiddlewareAndInvokeHandling(
            RequestDelegate requestDelegate,
            VolatileResourceManager volatileResourceManager
        )
        {
            var transactionScopeUnitOfWorkMiddleware = new TransactionScopeUnitOfWorkDependencyInjectionMiddleware(
                requestDelegate,
                transactionScopeEnlistmentAction: volatileResourceManager.EnlistIntoTransactionScope
            );

            var httpContext = new DefaultHttpContext();
            var unitOfWork = IoC.Resolve<IUnitOfWork>();

            await transactionScopeUnitOfWorkMiddleware.InvokeAsync(httpContext, unitOfWork);
        }
    }
}
#endif