using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CoreDdd.Domain.Events;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middlewares
{
    /// <summary>
    /// This class is a base class for unit of work middlewares. There are two way to define a middleware:
    /// 1) A middleware which does not implement a <see cref="IMiddleware"/> interface (used by ASP.NET Core Dependency injection IoC).
    /// 2) A middleware which implements a <see cref="IMiddleware"/> interface (used by Castle Windsor IoC and potentially other IoC containers).
    /// This class contains a code which is shared between those two middleware implementations.
    /// </summary>
    public abstract class BaseUnitOfWorkMiddleware
    {
        private readonly IsolationLevel _isolationLevel;
        private readonly IEnumerable<Regex> _getOrHeadRequestPathsWithoutTransaction;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="isolationLevel">An isolation level for the transaction</param>
        /// <param name="getOrHeadRequestPathsWithoutTransaction">List of GET or HEAD request path regexes for which the transaction will not be created</param>
        protected BaseUnitOfWorkMiddleware(
            IsolationLevel isolationLevel,
            IEnumerable<Regex> getOrHeadRequestPathsWithoutTransaction
            )
        {
            _isolationLevel = isolationLevel;
            _getOrHeadRequestPathsWithoutTransaction = getOrHeadRequestPathsWithoutTransaction;
        }

        /// <summary>
        /// Invokes the middleware operation.
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="next">Request delegate</param>
        /// <param name="unitOfWork">An instance of unit of work</param>
        /// <returns></returns>
        protected async Task InvokeAsync(HttpContext context, RequestDelegate next, IUnitOfWork unitOfWork)
        {
            if (_getOrHeadRequestPathsWithoutTransaction != null 
                && (context.Request.Method == WebRequestMethods.Http.Get || context.Request.Method == WebRequestMethods.Http.Head) 
                && _getOrHeadRequestPathsWithoutTransaction.Any(x => x.IsMatch(context.Request.Path.Value)))
            {
                await next.Invoke(context).ConfigureAwait(false);
                return;
            }

            unitOfWork.BeginTransaction(_isolationLevel);

            DomainEvents.ResetDelayedEventsStorage();

            try
            {
                await next.Invoke(context).ConfigureAwait(false);

                await unitOfWork.CommitAsync().ConfigureAwait(false);
            }
            catch
            {
                await unitOfWork.RollbackAsync().ConfigureAwait(false);
                throw;
            }

            DomainEvents.RaiseDelayedEvents();
        }
    }
}