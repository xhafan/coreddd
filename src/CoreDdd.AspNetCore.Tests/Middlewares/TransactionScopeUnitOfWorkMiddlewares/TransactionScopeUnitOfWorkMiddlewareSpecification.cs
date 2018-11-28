using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middlewares;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TransactionScopes;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Tests.Middlewares.TransactionScopeUnitOfWorkMiddlewares
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
