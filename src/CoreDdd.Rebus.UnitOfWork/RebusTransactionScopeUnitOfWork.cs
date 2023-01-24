using System;
using System.Transactions;
using CoreDdd.UnitOfWorks;
using Rebus.Pipeline;

namespace CoreDdd.Rebus.UnitOfWork
{
    /// <summary>
    /// Support for CoreDdd's unit of work within a transaction scope for Rebus.UnitOfWork package.
    /// For a unit of work without a transaction scope, please see <see cref="RebusUnitOfWork"/>.
    /// Please note that a transaction scope is not needed to ensure messages published or sent from a message handler 
    /// are not published or sent when there is an error during the message handling, and 
    /// using <see cref="RebusUnitOfWork"/> is sufficient for this scenario.
    /// This class allows to enlist another resource manager into the transaction scope.
    /// </summary>
    public class RebusTransactionScopeUnitOfWork
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IsolationLevel _isolationLevel;
        private readonly Action<TransactionScope> _transactionScopeEnlistmentAction;

        /// <summary>
        /// Initializes the class. Needs to be called at the application start.
        /// </summary>
        /// <param name="unitOfWorkFactory">A unit of work factory</param>
        /// <param name="isolationLevel">Isolation level for the transaction scope</param>
        /// <param name="transactionScopeEnlistmentAction">An enlistment action for the transaction scope. Use to enlist another resource manager
        /// into the transaction scope</param>
        public RebusTransactionScopeUnitOfWork(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            Action<TransactionScope> transactionScopeEnlistmentAction = null
            )
        {
            _transactionScopeEnlistmentAction = transactionScopeEnlistmentAction;
            _unitOfWorkFactory = unitOfWorkFactory;
            _isolationLevel = isolationLevel;
        }

        /// <summary>
        /// Creates a new transaction scope and a new unit of work.
        /// </summary>
        /// <param name="messageContext">Rebus message context</param>
        /// <returns>A value tuple of the transaction scope and the unit of work</returns>
        public (TransactionScope TransactionScope, IUnitOfWork UnitOfWork) Create(IMessageContext messageContext)
        {
            if (_unitOfWorkFactory == null)
            {
                throw new InvalidOperationException("UnitOfWork factory is not set.");
            }
            var unitOfWork = _unitOfWorkFactory.Create();
            var transactionScope = _CreateTransactionScope();
            _transactionScopeEnlistmentAction?.Invoke(transactionScope);
            unitOfWork.BeginTransaction();
            return (transactionScope, unitOfWork);
        }

        /// <summary>
        /// Commits the unit of work and the transaction scope.
        /// </summary>
        /// <param name="messageContext">Rebus message context</param>
        /// <param name="transactionScopeUnitOfWork">A value tuple of the transaction scope and the unit of work</param>
        public void Commit(
            IMessageContext messageContext, 
            (TransactionScope TransactionScope, IUnitOfWork UnitOfWork) transactionScopeUnitOfWork
            )
        {
            transactionScopeUnitOfWork.UnitOfWork.Commit();
            transactionScopeUnitOfWork.TransactionScope.Complete();
        }

        /// <summary>
        /// Rolls back the unit of work and the transaction scope.
        /// </summary>
        /// <param name="messageContext">Rebus message context</param>
        /// <param name="transactionScopeUnitOfWork">A value tuple of the transaction scope and the unit of work</param>
        public void Rollback(
            IMessageContext messageContext, 
            (TransactionScope TransactionScope, IUnitOfWork UnitOfWork) transactionScopeUnitOfWork
            )
        {
            transactionScopeUnitOfWork.UnitOfWork.Rollback();
        }

        /// <summary>
        /// Cleans the unit of work and the transaction scope.
        /// </summary>
        /// <param name="messageContext">Rebus message context</param>
        /// <param name="transactionScopeUnitOfWork">A value tuple of the transaction scope and the unit of work</param>
        public void Cleanup(
            IMessageContext messageContext, 
            (TransactionScope TransactionScope, IUnitOfWork UnitOfWork) transactionScopeUnitOfWork
            )
        {
            _unitOfWorkFactory.Release(transactionScopeUnitOfWork.UnitOfWork);
            transactionScopeUnitOfWork.TransactionScope.Dispose();
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