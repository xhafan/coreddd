using System;
using System.Data;
using System.Web;
using Core.Utilities.NHibernate;
using NHibernate;

namespace Core.Commons
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;

        [ThreadStatic]
        private static IUnitOfWork _threadUnitOfWork;
        private static readonly object UnitOfWorkKey = new object();

        private static ISessionFactory _sessionFactory;
        private static readonly object SessionFactoryLock = new object();

        public UnitOfWork(ISession session)
        {
            _session = session;
        }

        public static IUnitOfWork Current
        {
            get
            {
                if (!RunningInWeb)
                {
                    return _threadUnitOfWork ?? (_threadUnitOfWork = Create());
                }
                var httpContextUnitOfWork = HttpContext.Current.Items[UnitOfWorkKey] as IUnitOfWork;
                if (httpContextUnitOfWork == null)
                {
                    httpContextUnitOfWork = Create();
                    HttpContext.Current.Items[UnitOfWorkKey] = httpContextUnitOfWork;
                }
                return httpContextUnitOfWork;
            }
            internal set
            {
                if (!RunningInWeb)
                {
                    _threadUnitOfWork = value;
                }
                else
                {
                    HttpContext.Current.Items[UnitOfWorkKey] = value;
                }
            }
        }

        private static IUnitOfWork Create()
        {
            var session = SessionFactory.OpenSession();
            return new UnitOfWork(session);
        } 

        public void Dispose()
        {
            _session.Dispose();
        }

        public void Flush()
        {
            _session.Flush();
        }

        public bool IsInActiveTransaction
        {
            get { return _session.Transaction.IsActive; }
        }

        public ITransaction BeginTransaction()
        {
            return _session.BeginTransaction();
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return _session.BeginTransaction(isolationLevel);
        }

        public void TransactionalFlush()
        {
            TransactionalFlush(IsolationLevel.ReadCommitted);
        }

        public void TransactionalFlush(IsolationLevel isolationLevel)
        {
            var tx = BeginTransaction(isolationLevel);
            try
            {
                //forces a flush of the current unit of work
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
            }
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    lock (SessionFactoryLock)
                    {
                        if (_sessionFactory == null)
                        {
                            _sessionFactory = NHibernateUtilities.ConfigureNHibernate();
                        }
                    }
                }
                return _sessionFactory;
            }
            internal set
            {
                _sessionFactory = value;  
            }
        }
        
        private static bool RunningInWeb
        {
            get { return HttpContext.Current != null; }
        }    
    }
}