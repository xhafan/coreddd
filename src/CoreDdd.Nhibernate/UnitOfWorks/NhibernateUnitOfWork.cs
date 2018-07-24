using System.Transactions;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.UnitOfWorks;
using NHibernate;

#if !NET40 && !NET45
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.UnitOfWorks
{
    public class NhibernateUnitOfWork : IUnitOfWork
    {
        private readonly INhibernateConfigurator _configurator;
        private bool _isInTransactionScope;

        public NhibernateUnitOfWork(INhibernateConfigurator configurator)
        {
            _configurator = configurator;
        }

        public ISession Session { get; private set; }

        public void BeginTransaction(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.Unspecified)
        {
            _isInTransactionScope = Transaction.Current != null;

            Session = _configurator.GetSessionFactory().OpenSession();

            if (_isInTransactionScope) return;

            Session.BeginTransaction(isolationLevel);
        }

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
                tx.Rollback();
                throw;
            }
            finally
            {
                tx.Dispose();
                Session.Dispose();
                Session = null;
            }
        }

        public void Rollback()
        {
            if (!_IsActive()) return;

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
                tx.Rollback();
            }
            finally
            {
                tx.Dispose();
                Session.Dispose();
                Session = null;
            }
        }

        public void Flush()
        {
            Session.Flush();
        }

#if !NET40 && !NET45
        public async Task CommitAsync()
        {
            await FlushAsync();

            if (_isInTransactionScope)
            {
                Session.Dispose();
                Session = null;
                return;
            }

            var tx = Session.Transaction;
            try
            {
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
            finally
            {
                tx.Dispose();
                Session.Dispose();
                Session = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (!_IsActive()) return;

            await FlushAsync();

            if (_isInTransactionScope)
            {
                Session.Dispose();
                Session = null;
                return;
            }

            var tx = Session.Transaction;
            try
            {
                await tx.RollbackAsync();
            }
            finally
            {
                tx.Dispose();
                Session.Dispose();
                Session = null;
            }
        }

        public Task FlushAsync()
        {
            return Session.FlushAsync();
        }
#endif

        public void Clear()
        {
            Session.Clear();
        }

        public void Dispose()
        {
            if (!_IsActive()) return;

            Commit();
        }

        private bool _IsActive()
        {
            return Session != null;
        }
    }
}