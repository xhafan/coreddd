using System.Data;
using System.Threading.Tasks;
using CoreDdd.Domain.Events;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    public class UnitOfWorkMiddleware : IMiddleware
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IsolationLevel _isolationLevel;

        public UnitOfWorkMiddleware(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
            )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _isolationLevel = isolationLevel;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var unitOfWork = _unitOfWorkFactory.Create();

            try
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
            finally
            {
                _unitOfWorkFactory.Release(unitOfWork);
            }
        }
    }
}