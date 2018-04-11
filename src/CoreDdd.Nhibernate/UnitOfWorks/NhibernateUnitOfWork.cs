using System.Transactions;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.UnitOfWorks;
using NHibernate;

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

        public void Clear()
        {
            Session.Clear();
        }

        public void Dispose()
        {
            if (!_isActive()) return;

            Commit();

            bool _isActive()
            {
                return Session != null;
            }
        }
    }
}