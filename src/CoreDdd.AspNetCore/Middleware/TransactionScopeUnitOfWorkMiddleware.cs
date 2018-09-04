using System;
using System.Threading.Tasks;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CoreDdd.AspNetCore.Middleware
{
    /// <summary>
    /// Use this middleware when using TransactionScope and IoC containers like Castle.Windsor or similar
    /// </summary>
    public class TransactionScopeUnitOfWorkMiddleware : BaseTransactionScopeUnitOfWorkMiddleware, IMiddleware
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public TransactionScopeUnitOfWorkMiddleware(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope> transactionScopeEnlistmentAction = null
            ) : base(isolationLevel, transactionScopeEnlistmentAction)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

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