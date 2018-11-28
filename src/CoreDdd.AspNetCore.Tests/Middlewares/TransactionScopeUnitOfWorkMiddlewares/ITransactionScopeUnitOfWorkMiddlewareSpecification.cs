using System.Threading.Tasks;
using IntegrationTestsShared.TransactionScopes;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Tests.Middlewares.TransactionScopeUnitOfWorkMiddlewares
{
    public interface ITransactionScopeUnitOfWorkMiddlewareSpecification
    {
        Task CreateMiddlewareAndInvokeHandling(
            RequestDelegate requestDelegate,
            VolatileResourceManager volatileResourceManager
            );
    }
}
