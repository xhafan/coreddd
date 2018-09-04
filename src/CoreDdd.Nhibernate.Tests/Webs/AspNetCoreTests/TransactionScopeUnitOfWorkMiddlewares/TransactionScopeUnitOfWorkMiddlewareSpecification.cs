#if NETCOREAPP
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middleware;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetCoreTests.TransactionScopeUnitOfWorkMiddlewares
{
    public class TransactionScopeUnitOfWorkMiddlewareSpecification : ITransactionScopeUnitOfWorkMiddlewareSpecification
    {
        public async Task CreateMiddlewareAndInvokeHandling(
            RequestDelegate requestDelegate,
            VolatileResourceManager volatileResourceManager
            )
        {
            var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
            var transactionScopeUnitOfWorkMiddleware = new TransactionScopeUnitOfWorkMiddleware(
                unitOfWorkFactory: unitOfWorkFactory,
                transactionScopeEnlistmentAction: volatileResourceManager.EnlistIntoTransactionScope
            );

            var httpContext = new DefaultHttpContext();

            await transactionScopeUnitOfWorkMiddleware.InvokeAsync(httpContext, requestDelegate);
        }
    }
}
#endif