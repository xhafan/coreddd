using System.Data;
using System.Threading.Tasks;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// Wraps a web request inside an unit of work transaction.
    /// Use this middleware when using ASP.NET Core Dependency Injection and not using TransactionScope.
    /// </summary>
    public class UnitOfWorkDependencyInjectionMiddleware : BaseUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="isolationLevel">An isolation level for the unit of work transaction</param>
        public UnitOfWorkDependencyInjectionMiddleware(
            RequestDelegate next,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
            ) : base(isolationLevel)
        {
            _next = next;
        }
        
        /// <summary>
        /// Invokes the middleware operation with an unit of work injected into the method by Dependency Injection IoC.
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="unitOfWork">An injected instance of unit of work per web request</param>
        /// <returns></returns>
        public Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            return InvokeAsync(context, _next, unitOfWork);
        }
    }
}