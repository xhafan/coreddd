using System;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.UnitOfWorks;
using NHibernate;

namespace CoreDdd.Nhibernate.UnitOfWorks
{
    public class NhibernateUnitOfWork : IUnitOfWork
    {
        private readonly INhibernateConfigurator _configurator;

        protected NhibernateUnitOfWork() {}

        public NhibernateUnitOfWork(INhibernateConfigurator configurator)
        {
            _configurator = configurator;
        }

        public ISession Session { get; private set; }

        public void BeginTransaction()
        {
            Session = _configurator.GetSessionFactory().OpenSession();
            Session.BeginTransaction();
        }

        public void Commit()
        {
            InvokeTransactionActionAndDisposeBothTransactionAndSession(tx =>
            {
                try
                {
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            });
        }

        public void Rollback()
        {
            if (!IsActive()) return;
            InvokeTransactionActionAndDisposeBothTransactionAndSession(tx => tx.Rollback());
        }

        private void InvokeTransactionActionAndDisposeBothTransactionAndSession(Action<ITransaction> action)
        {
            try
            {
                using (var tx = Session.Transaction)
                {
                    action(tx);
                }
            }
            finally
            {
                Session.Dispose();
                Session = null;
            }
        }

        public void Flush()
        {
            Session.Flush();
        }

        public bool IsActive()
        {
            return Session != null;
        }
    }
}