using System;
using System.Data;

#if !NET40 && !NET45
using System.Threading.Tasks;
#endif

namespace CoreDdd.UnitOfWorks
{
    /// <summary>
    /// Represents a unit of work which can start a transaction, commit it or roll it back.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begin a transaction with a given isolation level.
        /// </summary>
        /// <param name="isolationLevel">An isolation level</param>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        /// <summary>
        /// Commit a transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback a transaction.
        /// </summary>
        void Rollback();

#if !NET40 && !NET45
        /// <summary>
        /// Commit a transaction asynchronously.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Rollback a transaction asynchronously.
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();
#endif
    }
}