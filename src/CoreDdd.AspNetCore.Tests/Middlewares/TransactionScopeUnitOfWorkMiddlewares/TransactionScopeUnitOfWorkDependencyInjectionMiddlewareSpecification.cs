using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middlewares;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TransactionScopes;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Tests.Middlewares.TransactionScopeUnitOfWorkMiddlewares
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
