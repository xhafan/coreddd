using System;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middlewares
{
    /// <summary>
    /// Wraps a web request inside a transaction scope.
    /// Use this middleware when using TransactionScope and IoC containers like Castle.Windsor or similar.
    /// </summary>
    public class TransactionScopeUnitOfWorkMiddleware : BaseTransactionScopeUnitOfWorkMiddleware, IMiddleware
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="unitOfWorkFactory">An unit of work factory which creates an unit of work per web request</param>
        /// <param name="isolationLevel">An isolation level for the transaction scope</param>
        /// <param name="transactionScopeEnlistmentAction">An enlistment action for the transaction scope. Use to enlist another resource manager
        /// into the transaction scope</param>
        public TransactionScopeUnitOfWorkMiddleware(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope> transactionScopeEnlistmentAction = null
            ) : base(isolationLevel, transactionScopeEnlistmentAction)
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