using System.Data;
using System.Threading.Tasks;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// Use this middleware with IoC containers like Castle.Windsor or similar, and when not using TransactionScope
    /// </summary>
    public class UnitOfWorkMiddleware : BaseUnitOfWorkMiddleware, IMiddleware
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public UnitOfWorkMiddleware(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
            ) : base(isolationLevel)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var unitOfWork = _unitOfWorkFactory.Create();

            try
            {
                return InvokeAsync(context, next, unitOfWork);
            }
            finally
            {
                _unitOfWorkFactory.Release(unitOfWork);
            }
        }
    }
}