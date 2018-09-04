using System;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// Use this middleware when using Microsoft dependency injection and TransactionScope
    /// </summary>
    public class TransactionScopeUnitOfWorkMicrosoftDependencyInjectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IsolationLevel _isolationLevel;
        private readonly Action<TransactionScope> _transactionScopeEnlistmentAction;

        public TransactionScopeUnitOfWorkMicrosoftDependencyInjectionMiddleware(
            RequestDelegate next,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope> transactionScopeEnlistmentAction = null
            )
        {
            _next = next;
            _isolationLevel = isolationLevel;
            _transactionScopeEnlistmentAction = transactionScopeEnlistmentAction;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            using (var transactionScope = _CreateTransactionScope())
            {
                _transactionScopeEnlistmentAction?.Invoke(transactionScope);

                unitOfWork.BeginTransaction();

                try
                {
                    await _next.Invoke(context).ConfigureAwait(false);

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