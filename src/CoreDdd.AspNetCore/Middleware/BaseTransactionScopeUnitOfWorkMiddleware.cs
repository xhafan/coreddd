using System;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// This class is a base class for transaction scope middlewares. There are two way to define a middleware:
    /// 1) A middleware which does not implement a <see cref="IMiddleware"/> interface (used by ASP.NET Core Dependency injection IoC).
    /// 2) A middleware which implements a <see cref="IMiddleware"/> interface (used by Castle Windsor IoC and potentially other IoC containers).
    /// This class contains a code which is shared between those two middleware implementations.
    /// </summary>
    public abstract class BaseTransactionScopeUnitOfWorkMiddleware
    {
        private readonly IsolationLevel _isolationLevel;
        private readonly Action<TransactionScope> _transactionScopeEnlistmentAction;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="isolationLevel">An isolation level for the transaction scope</param>
        /// <param name="transactionScopeEnlistmentAction">An enlistment action for the transaction scope. Use to enlist another resource manager
        /// into the transaction scope</param>
        protected BaseTransactionScopeUnitOfWorkMiddleware(
            IsolationLevel isolationLevel,
            Action<TransactionScope> transactionScopeEnlistmentAction
        )
        {
            _isolationLevel = isolationLevel;
            _transactionScopeEnlistmentAction = transactionScopeEnlistmentAction;
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
            using (var transactionScope = _CreateTransactionScope())
            {
                _transactionScopeEnlistmentAction?.Invoke(transactionScope);

                unitOfWork.BeginTransaction();

                try
                {
                    await next.Invoke(context).ConfigureAwait(false);

                    await unitOfWork.CommitAsync().ConfigureAwait(false);
                    transactionScope.Complete();
                }
                catch
                {
                    await unitOfWork.RollbackAsync().ConfigureAwait(false);
                    throw;
                }
            }
        }

        private TransactionScope _CreateTransactionScope()
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = _isolationLevel },
                TransactionScopeAsyncFlowOption.Enabled
            );
        }
    }
}