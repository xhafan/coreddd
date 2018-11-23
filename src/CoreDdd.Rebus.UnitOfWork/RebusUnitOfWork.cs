using System;
using System.Data;
using CoreDdd.UnitOfWorks;
using Rebus.Pipeline;

namespace CoreDdd.Rebus.UnitOfWork
{
    /// <summary>
    /// Support for CoreDdd's unit of work for Rebus.UnitOfWork package.
    /// For a unit of work within a transaction scope, please see <see cref="RebusTransactionScopeUnitOfWork"/>.
    /// Please note that messages published or sent from a message handler 
    /// are not published or sent when there is an error during the message handling.
    /// </summary>
    public static class RebusUnitOfWork
    {
        private static IUnitOfWorkFactory _unitOfWorkFactory;
        private static IsolationLevel _isolationLevel;

        /// <summary>
        /// Initializes the class. Needs to be called at the application start.
        /// </summary>
        /// <param name="unitOfWorkFactory">A unit of work factory</param>
        /// <param name="isolationLevel">Isolation level for the transaction</param>
        public static void Initialize(
            IUnitOfWorkFactory unitOfWorkFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
        )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _isolationLevel = isolationLevel;
        }

        /// <summary>
        /// Creates a new unit of work.
        /// </summary>
        /// <param name="messageContext">Rebus message context</param>
        /// <returns>The unit of work</returns>
        public static IUnitOfWork Create(IMessageContext messageContext)
        {
            if (_unitOfWorkFactory == null)
            {
                throw new InvalidOperationException(
                    "RebusUnitOfWork has not been initialized! Please call RebusUnitOfWork.Initialize(...) before using it.");
            }
            var unitOfWork = _unitOfWorkFactory.Create();
            unitOfWork.BeginTransaction(_isolationLevel);
            return unitOfWork;
        }

        /// <summary>
        /// Commits the unit of work.
        /// </summary>
        /// <param name="messageContext">Rebus message context</param>
        /// <param name="unitOfWork">The unit of work</param>
        public static void Commit(IMessageContext messageContext, IUnitOfWork unitOfWork)
        {
            unitOfWork.Commit();
        }

        /// <summary>
        /// Rolls back the unit of work.
        /// </summary>
        /// <param name="messageContext">Rebus message context</param>
        /// <param name="unitOfWork">The unit of work</param>
        public static void Rollback(IMessageContext messageContext, IUnitOfWork unitOfWork)
        {
            unitOfWork.Rollback();
        }

        /// <summary>
        /// Cleans the unit of work.
        /// </summary>
        /// <param name="messageContext">Rebus message context</param>
        /// <param name="unitOfWork">The unit of work</param>
        public static void Cleanup(IMessageContext messageContext, IUnitOfWork unitOfWork)
        {
            _unitOfWorkFactory.Release(unitOfWork);
        }
    }
}