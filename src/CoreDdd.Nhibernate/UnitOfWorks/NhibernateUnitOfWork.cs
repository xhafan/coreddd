using CoreDdd.Nhibernate.Configurations;
using CoreDdd.UnitOfWorks;
using NHibernate;

namespace CoreDdd.Nhibernate.UnitOfWorks
{
    public class NhibernateUnitOfWork : IUnitOfWork
    {
        private readonly INhibernateConfigurator _configurator;

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