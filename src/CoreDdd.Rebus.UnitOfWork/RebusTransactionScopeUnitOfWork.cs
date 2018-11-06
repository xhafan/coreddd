using System;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Rebus.Pipeline;

namespace CoreDdd.Rebus.UnitOfWork
{
    public static class RebusTransactionScopeUnitOfWork
    {
        private static IUnitOfWorkFactory _unitOfWorkFactory;
        private static IsolationLevel _isolationLevel;
        private static Action<TransactionScope> _transactionScopeEnlistmentAction;

        public static void Initialize(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope> transactionScopeEnlistmentAction = null
            )
        {
            _transactionScopeEnlistmentAction = transactionScopeEnlistmentAction;
            _unitOfWorkFactory = unitOfWorkFactory;
            _isolationLevel = isolationLevel;
        }

        public static (TransactionScope transactionScope, IUnitOfWork unitOfWork) Create(IMessageContext arg)
        {
            if (_unitOfWorkFactory == null)
            {
                throw new InvalidOperationException(
                    "RebusTransactionScopeUnitOfWork has not been initialized! Please call RebusTransactionScopeUnitOfWork.Initialize(...) before using it.");
            }
            var unitOfWork = _unitOfWorkFactory.Create();
            var transactionScope = _CreateTransactionScope();
            _transactionScopeEnlistmentAction?.Invoke(transactionScope);
            unitOfWork.BeginTransaction();
            return (transactionScope, unitOfWork);
        }

        public static void Commit(
            IMessageContext messageContext, 
            (TransactionScope transactionScope, IUnitOfWork unitOfWork) transactionScopeUnitOfWork
            )
        {
            transactionScopeUnitOfWork.unitOfWork.Commit();
            transactionScopeUnitOfWork.transactionScope.Complete();
        }

        public static void Rollback(
            IMessageContext messageContext, 
            (TransactionScope transactionScope, IUnitOfWork unitOfWork) transactionScopeUnitOfWork
            )
        {
            transactionScopeUnitOfWork.unitOfWork.Rollback();
        }

        public static void Cleanup(
            IMessageContext messageContext, 
            (TransactionScope transactionScope, IUnitOfWork unitOfWork) transactionScopeUnitOfWork
            )
        {
            _unitOfWorkFactory.Release(transactionScopeUnitOfWork.unitOfWork);
            transactionScopeUnitOfWork.transactionScope.Dispose();
        }

        private static TransactionScope _CreateTransactionScope()
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = _isolationLevel },
                TransactionScopeAsyncFlowOption.Enabled
            );
        }
    }
}