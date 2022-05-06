using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middlewares
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
        /// <param name="getOrHeadRequestPathsWithoutTransaction">List of GET or HEAD request path regexes for which the transaction will not be created</param>
        public UnitOfWorkDependencyInjectionMiddleware(
            RequestDelegate next,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            IEnumerable<Regex> getOrHeadRequestPathsWithoutTransaction = null
            ) : base(isolationLevel, getOrHeadRequestPathsWithoutTransaction)
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