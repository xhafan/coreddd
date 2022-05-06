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
        /// <param name="getOrHeadRequestPathsWithoutTransaction">List of GET or HEAD request path regexes for which the transaction will not be created</param>
        public UnitOfWorkMiddleware(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            IEnumerable<Regex> getOrHeadRequestPathsWithoutTransaction = null
        ) : base(isolationLevel, getOrHeadRequestPathsWithoutTransaction)
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