using System.Data;
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

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="isolationLevel">An isolation level for the transaction</param>
        protected BaseUnitOfWorkMiddleware(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
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