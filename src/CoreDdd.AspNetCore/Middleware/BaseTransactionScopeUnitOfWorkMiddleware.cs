using System;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    public class BaseTransactionScopeUnitOfWorkMiddleware
    {
        private readonly IsolationLevel _isolationLevel;
        private readonly Action<TransactionScope> _transactionScopeEnlistmentAction;

        protected BaseTransactionScopeUnitOfWorkMiddleware(
            IsolationLevel isolationLevel,
            Action<TransactionScope> transactionScopeEnlistmentAction
        )
        {
            _isolationLevel = isolationLevel;
            _transactionScopeEnlistmentAction = transactionScopeEnlistmentAction;
        }

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