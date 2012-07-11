using System;
using System.Data;
using System.Web;
using NHibernate;

namespace Core.Commons
{
    public class UnitOfWork
    {
        private ISession _session;

        [ThreadStatic]
        private static UnitOfWork _threadUnitOfWork;
        private static readonly object UnitOfWorkKey = new object();

        private static ISessionFactory _sessionFactory;

        public static void Initialize(INHibernateConfigurator configurator)
        {
            _sessionFactory = configurator.GetSessionFactory();
        }
        
        internal UnitOfWork(ISession session)
        {
            _session = session;
        }

        public static UnitOfWork Current
        {
            get
            {
                if (!RunningInWeb)
                {
                    return _threadUnitOfWork ?? (_threadUnitOfWork = Create());
                }
                var httpContextUnitOfWork = HttpContext.Current.Items[UnitOfWorkKey] as UnitOfWork;
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

        public static ISession CurrentSession
        {
            get { return Current._session; }
            internal set { Current._session = value; }
        }

        public static bool IsStarted
        {
            get
            {
                if (!RunningInWeb)
                {
                    return _threadUnitOfWork != null && _threadUnitOfWork._session != null;
                }
                var unitOfWork = HttpContext.Current.Items[UnitOfWorkKey];
                return unitOfWork != null && ((UnitOfWork)unitOfWork)._session != null;
            } 
        }

        private static UnitOfWork Create()
        {
            var session = SessionFactory.OpenSession();
            return new UnitOfWork(session);
        } 

        public void Dispose()
        {
            _session.Dispose();
            _session = null;
        }

        public void Flush()
        {
            _session.Flush();
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

        public void TransactionalRollback()
        {
            var tx = _session.Transaction;
            tx.Rollback();
            tx.Dispose();
        }

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    throw new InvalidOperationException("Session factory has not been initialized! Please call UnitOfWork.Initialize(...) before using it.");
                }
                return _sessionFactory;
            }
        }
        
        private static bool RunningInWeb
        {
            get { return HttpContext.Current != null; }
        }    
    }
}