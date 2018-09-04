using System.Data;
using System.Threading.Tasks;
using CoreDdd.Domain.Events;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    public class BaseUnitOfWorkMiddleware
    {
        private readonly IsolationLevel _isolationLevel;

        protected BaseUnitOfWorkMiddleware(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }

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