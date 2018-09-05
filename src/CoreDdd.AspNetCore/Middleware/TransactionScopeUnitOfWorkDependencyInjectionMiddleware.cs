using System;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// Use this middleware when using ASP.NET Core Dependency Injection and TransactionScope
    /// </summary>
    public class TransactionScopeUnitOfWorkDependencyInjectionMiddleware : BaseTransactionScopeUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;

        public TransactionScopeUnitOfWorkDependencyInjectionMiddleware(
            RequestDelegate next,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope> transactionScopeEnlistmentAction = null
            ) : base(isolationLevel, transactionScopeEnlistmentAction)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            return InvokeAsync(context, _next, unitOfWork);
        }
    }
}