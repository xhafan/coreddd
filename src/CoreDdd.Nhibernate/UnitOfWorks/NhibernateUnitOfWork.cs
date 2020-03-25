using System;
using System.Transactions;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.UnitOfWorks;
using NHibernate;

#if !NET40 && !NET45
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.UnitOfWorks
{
    /// <summary>
    /// Unit of work wrapper around NHibernate session, which can start a transaction, commit it or roll it back.
    /// When an ambient transaction scope is detected, the transaction is not started on the session, and is not 
    /// committed or rolled back, as these are handled by the transaction scope.
    /// </summary>
    public class NhibernateUnitOfWork : IUnitOfWork
    {
        private readonly INhibernateConfigurator _configurator;
        private bool _isInTransactionScope;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="configurator">An instance of NHibernate configurator</param>
        public NhibernateUnitOfWork(INhibernateConfigurator configurator)
        {
            _configurator = configurator;
        }

        /// <summary>
        /// NHibernate session associated with the unit of work.
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// Creates a new NHibernate session and starts a transaction if there is no ambient transaction scope.
        /// If there is an ambient transaction scope, the transaction is not started as the transaction is 
        /// handled by the transaction scope.
        /// </summary>
        /// <param name="isolationLevel">Isolation level for the transaction</param>
        public void BeginTransaction(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.Unspecified)
        {
            _isInTransactionScope = Transaction.Current != null;

            Session = _configurator.GetSessionFactory().OpenSession();

            if (_isInTransactionScope) return;

            Session.BeginTransaction(isolationLevel);
        }
        
        /// <summary>
        /// Flushes the NHibernate session and commits the transaction if there is no ambient transaction scope.
        /// If there is an ambient transaction scope, the commit is done by the transaction scope.
        /// </summary>
        public void Commit()
        {
            Flush();

            if (_isInTransactionScope)
            {
                Session.Dispose();
                Session = null;
                return;
            }

            var tx = Session.Transaction;
            try
            {
                tx.Commit();
            }
            catch
            {
                try { tx.Rollback(); } catch { /* ignored */ }
                throw;
            }
            finally
            {
                tx.Dispose();
                Session.Dispose();
                Session = null;
            }
        }

        /// <summary>
        /// Rolls back the transaction if there is no ambient transaction scope.
        /// If there is an ambient transaction scope, the rollback is done by the transaction scope.
        /// </summary>
        public void Rollback()
        {
            if (!_IsActive()) return;

            if (_isInTransactionScope)
            {
                Session.Dispose();
                Session = null;
                return;
            }

            var tx = Session.Transaction;
            try
            {
                tx.Rollback();
            }
            catch
            {
                // ignored
            }
            finally
            {
                tx.Dispose();
                Session.Dispose();
                Session = null;
            }
        }

        /// <summary>
        /// Flushes the NHibernate session, that is it inserts, updates and deletes session's entities into the database.
        /// </summary>
        public void Flush()
        {
            Session.Flush();
        }

#if !NET40 && !NET45
        /// <summary>
        /// Asynchronously flushes the NHibernate session and commits the transaction if there is no ambient transaction scope.
        /// If there is an ambient transaction scope, the commit is done by the transaction scope.
        /// </summary>
        public async Task CommitAsync()
        {
            await FlushAsync().ConfigureAwait(false);

            if (_isInTransactionScope)
            {
                Session.Dispose();
                Session = null;
                return;
            }

            var tx = Session.Transaction;
            try
            {
                await tx.CommitAsync().ConfigureAwait(false);
            }
            catch
            {
                try { await tx.RollbackAsync().ConfigureAwait(false); } catch { /* ignored */ }
                throw;
            }
            finally
            {
                tx.Dispose();
                Session.Dispose();
                Session = null;
            }
        }

        /// <summary>
        /// Rolls back the transaction if there is no ambient transaction scope.
        /// If there is an ambient transaction scope, the rollback is done by the transaction scope.
        /// </summary>
        public async Task RollbackAsync()
        {
            if (!_IsActive()) return;

            if (_isInTransactionScope)
            {
                Session.Dispose();
                Session = null;
                return;
            }

            var tx = Session.Transaction;
            try
            {
                await tx.RollbackAsync().ConfigureAwait(false);
            }
            catch
            {
                // ignored
            }
            finally
            {
                tx.Dispose();
                Session.Dispose();
                Session = null;
            }
        }

        /// <summary>
        /// Asynchronously flushes the NHibernate session, that is it inserts, updates and deletes session's entities into the database.
        /// </summary>
        public Task FlushAsync()
        {
            return Session.FlushAsync();
        }
#endif

        /// <summary>
        /// Clears the NHibernate session. Any changes in session's entities will be discarded.
        /// </summary>
        public void Clear()
        {
            Session.Clear();
        }

        /// <summary>
        /// Disposes resources.
        /// </summary>
        public void Dispose() // https://stackoverflow.com/a/898867/379279
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Over-ridable dispose method. 
        /// </summary>
        /// <param name="disposing">true - the method call comes from a Dispose method; false - the method call comes from a finalizer</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_IsActive()) return;
                
                Commit();
            }
        }

        private bool _IsActive()
        {
            return Session != null;
        }
    }
}