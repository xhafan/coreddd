﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middlewares
{
    /// <summary>
    /// Wraps a web request inside a transaction scope.
    /// Use this middleware when using ASP.NET Core Dependency Injection and TransactionScope.
    /// </summary>
    public class TransactionScopeUnitOfWorkDependencyInjectionMiddleware : BaseTransactionScopeUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="next">Request delegate</param>
        /// <param name="isolationLevel">An isolation level for the transaction scope</param>
        /// <param name="getOrHeadRequestPathsWithoutTransaction">List of GET or HEAD request path regexes for which the transaction will not be created</param>
        /// <param name="transactionScopeEnlistmentAction">An enlistment action for the transaction scope. Use to enlist another resource manager
        /// into the transaction scope</param>
        public TransactionScopeUnitOfWorkDependencyInjectionMiddleware(
            RequestDelegate next,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            IEnumerable<Regex>? getOrHeadRequestPathsWithoutTransaction = null,
            Action<TransactionScope>? transactionScopeEnlistmentAction = null
            ) : base(isolationLevel, getOrHeadRequestPathsWithoutTransaction, transactionScopeEnlistmentAction)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware operation with an unit of work injected into the method by Dependency Injection IoC.
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="unitOfWork">An injected instance of unit of work per web request</param>
        /// <returns></returns>
        public Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            return InvokeAsync(context, _next, unitOfWork);
        }
    }
}