﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middlewares
{
    /// <summary>
    /// This class is a base class for transaction scope middlewares. There are two ways to define a middleware:
    /// 1) A middleware which does not implement a <see cref="IMiddleware"/> interface (used by ASP.NET Core Dependency injection IoC).
    /// 2) A middleware which implements a <see cref="IMiddleware"/> interface (used by Castle Windsor IoC and potentially other IoC containers).
    /// This class contains a code which is shared between those two middleware implementations.
    /// </summary>
    public abstract class BaseTransactionScopeUnitOfWorkMiddleware
    {
        private readonly IsolationLevel _isolationLevel;
        private readonly IEnumerable<Regex>? _getOrHeadRequestPathsWithoutTransaction;
        private readonly Action<TransactionScope>? _transactionScopeEnlistmentAction;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="isolationLevel">An isolation level for the transaction scope</param>
        /// <param name="getOrHeadRequestPathsWithoutTransaction">List of GET or HEAD request path regexes for which the transaction will not be created</param>
        /// <param name="transactionScopeEnlistmentAction">An enlistment action for the transaction scope. Use to enlist another resource manager
        /// into the transaction scope</param>
        protected BaseTransactionScopeUnitOfWorkMiddleware(
            IsolationLevel isolationLevel,
            IEnumerable<Regex>? getOrHeadRequestPathsWithoutTransaction,
            Action<TransactionScope>? transactionScopeEnlistmentAction
        )
        {
            _isolationLevel = isolationLevel;
            _getOrHeadRequestPathsWithoutTransaction = getOrHeadRequestPathsWithoutTransaction;
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
            if (_getOrHeadRequestPathsWithoutTransaction != null
                && (context.Request.Method == WebRequestMethods.Http.Get || context.Request.Method == WebRequestMethods.Http.Head)
                && context.Request.Path.Value != null
                && _getOrHeadRequestPathsWithoutTransaction.Any(x => x.IsMatch(context.Request.Path.Value)))
            {
                await next.Invoke(context).ConfigureAwait(false);
                return;
            }

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