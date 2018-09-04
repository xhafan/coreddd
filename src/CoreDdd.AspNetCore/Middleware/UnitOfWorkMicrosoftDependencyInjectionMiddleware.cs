using System.Data;
using System.Threading.Tasks;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// Use this middleware when using Microsoft dependency injection and not using TransactionScope
    /// </summary>
    public class UnitOfWorkMicrosoftDependencyInjectionMiddleware : BaseUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;

        public UnitOfWorkMicrosoftDependencyInjectionMiddleware(
            RequestDelegate next,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
            ) : base(isolationLevel)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            return InvokeAsync(context, _next, unitOfWork);
        }
    }
}