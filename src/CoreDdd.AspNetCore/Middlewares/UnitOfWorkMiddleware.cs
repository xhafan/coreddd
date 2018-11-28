using System.Data;
using System.Threading.Tasks;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middlewares
{
    /// <summary>
    /// Wraps a web request inside an unit of work transaction.
    /// Use this middleware with IoC containers like Castle.Windsor or similar, and when not using TransactionScope.
    /// </summary>
    public class UnitOfWorkMiddleware : BaseUnitOfWorkMiddleware, IMiddleware
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="unitOfWorkFactory">An unit of work factory which creates an unit of work per web request</param>
        /// <param name="isolationLevel">An isolation level for the unit of work transaction</param>
        public UnitOfWorkMiddleware(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
            ) : base(isolationLevel)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Creates a new unit of work and invokes the middleware operation.
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="next">Request delegate</param>
        /// <returns></returns>
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