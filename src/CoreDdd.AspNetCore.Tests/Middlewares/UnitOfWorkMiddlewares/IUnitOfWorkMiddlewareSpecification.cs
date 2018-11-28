using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Tests.Middlewares.UnitOfWorkMiddlewares
{
    public interface IUnitOfWorkMiddlewareSpecification
    {
        Task CreateMiddlewareAndInvokeHandling(RequestDelegate requestDelegate);
    }
}
