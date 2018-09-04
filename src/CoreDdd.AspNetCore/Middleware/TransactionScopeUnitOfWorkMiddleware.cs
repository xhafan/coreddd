﻿using System;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// Use this middleware when using TransactionScope and IoC containers like Castle.Windsor or similar
    /// </summary>
    public class TransactionScopeUnitOfWorkMiddleware : IMiddleware
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IsolationLevel _isolationLevel;
        private readonly Action<TransactionScope> _transactionScopeEnlistmentAction;

        public TransactionScopeUnitOfWorkMiddleware(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope> transactionScopeEnlistmentAction = null
            )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _isolationLevel = isolationLevel;
            _transactionScopeEnlistmentAction = transactionScopeEnlistmentAction;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (var transactionScope = _CreateTransactionScope())
            {
                _transactionScopeEnlistmentAction?.Invoke(transactionScope);

                var unitOfWork = _unitOfWorkFactory.Create();
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
                finally
                {
                    _unitOfWorkFactory.Release(unitOfWork);
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