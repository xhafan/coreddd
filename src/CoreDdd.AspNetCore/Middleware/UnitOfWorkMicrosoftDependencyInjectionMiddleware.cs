using System.Data;
using System.Threading.Tasks;
using CoreDdd.Domain.Events;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// Use this middleware when using Microsoft dependency injection and not using TransactionScope
    /// </summary>
    public class UnitOfWorkMicrosoftDependencyInjectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IsolationLevel _isolationLevel;

        public UnitOfWorkMicrosoftDependencyInjectionMiddleware(
            RequestDelegate next,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
            )
        {
            _next = next;
            _isolationLevel = isolationLevel;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            unitOfWork.BeginTransaction(_isolationLevel);

            DomainEvents.ResetDelayedEventsStorage();

            try
            {
                await _next.Invoke(context).ConfigureAwait(false);

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