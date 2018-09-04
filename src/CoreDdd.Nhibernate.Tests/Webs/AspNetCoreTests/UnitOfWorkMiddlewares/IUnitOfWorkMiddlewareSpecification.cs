#if NETCOREAPP
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetCoreTests.UnitOfWorkMiddlewares
{
    public interface IUnitOfWorkMiddlewareSpecification
    {
        Task CreateMiddlewareAndInvokeHandling(RequestDelegate requestDelegate);
    }
}
#endif