#if NETCOREAPP
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetCoreTests.TransactionScopeUnitOfWorkMiddlewares
{
    public interface ITransactionScopeUnitOfWorkMiddlewareSpecification
    {
        Task CreateMiddlewareAndInvokeHandling(
            RequestDelegate requestDelegate,
            VolatileResourceManager volatileResourceManager
            );
    }
}
#endif