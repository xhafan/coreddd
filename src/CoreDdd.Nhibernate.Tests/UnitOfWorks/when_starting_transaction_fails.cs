using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.UnitOfWorks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Engine;
using NHibernate.Metadata;
using NHibernate.Stat;
using NHibernate.Transaction;
using NHibernate.Type;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_starting_transaction_fails
    {
        private NhibernateUnitOfWork _unitOfWork;

        [Test]
        public void unit_of_work_can_be_gracefully_disposed()
        {
            _unitOfWork = new NhibernateUnitOfWork(new TestNhibernateConfigurator());
            try
            {
                _unitOfWork.BeginTransaction();
            }
            catch
            {
                // ignored
            }

            _unitOfWork.Dispose();
        }

        #region TestNhibernateConfigurator
        private class TestNhibernateConfigurator : INhibernateConfigurator
        {
            public ISessionFactory GetSessionFactory()
            {
                return new TestSessionFactory();
            }

            public Configuration GetConfiguration()
            {
                throw new System.NotImplementedException();
            }
        }

        private class TestSession : ISession
        {
#pragma warning disable CS0169 // The field is never used
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
            private DbConnection _;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
#pragma warning restore CS0169 // The field is never used

            public void Dispose()
            {
            }

            public Task FlushAsync(CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<bool> IsDirtyAsync(CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> LoadAsync(Type theType, object id, LockMode lockMode,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> LoadAsync(string entityName, object id, LockMode lockMode,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> LoadAsync(Type theType, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<T> LoadAsync<T>(object id, LockMode lockMode, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<T> LoadAsync<T>(object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> LoadAsync(string entityName, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task LoadAsync(object obj, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task ReplicateAsync(object obj, ReplicationMode replicationMode,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task ReplicateAsync(string entityName, object obj, ReplicationMode replicationMode,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> SaveAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task SaveAsync(object obj, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> SaveAsync(string entityName, object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task SaveAsync(string entityName, object obj, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task SaveOrUpdateAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task SaveOrUpdateAsync(string entityName, object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task SaveOrUpdateAsync(string entityName, object obj, object id,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task UpdateAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task UpdateAsync(object obj, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task UpdateAsync(string entityName, object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task UpdateAsync(string entityName, object obj, object id,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> MergeAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> MergeAsync(string entityName, object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<T> MergeAsync<T>(T entity, CancellationToken cancellationToken = new CancellationToken()) where T : class
            {
                throw new NotImplementedException();
            }

            public Task<T> MergeAsync<T>(string entityName, T entity, CancellationToken cancellationToken = new CancellationToken()) where T : class
            {
                throw new NotImplementedException();
            }

            public Task PersistAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task PersistAsync(string entityName, object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task DeleteAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task DeleteAsync(string entityName, object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<int> DeleteAsync(string query, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<int> DeleteAsync(string query, object value, IType type, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<int> DeleteAsync(string query, object[] values, IType[] types,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task LockAsync(object obj, LockMode lockMode, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task LockAsync(string entityName, object obj, LockMode lockMode,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task RefreshAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task RefreshAsync(object obj, LockMode lockMode, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<IQuery> CreateFilterAsync(object collection, string queryString,
                CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> GetAsync(Type clazz, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> GetAsync(Type clazz, object id, LockMode lockMode, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<object> GetAsync(string entityName, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<T> GetAsync<T>(object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<T> GetAsync<T>(object id, LockMode lockMode, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task<string> GetEntityNameAsync(object obj, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

#if !NET40 && !NET45
            public ISharedSessionBuilder SessionWithOptions()
            {
                throw new NotImplementedException();
            }
#endif
            public void Flush()
            {
            }
#if !NET40 && !NET45
            DbConnection ISession.Disconnect()
            {
                throw new NotImplementedException();
            }
#endif
            public IDbConnection Disconnect()
            {
                throw new NotImplementedException();
            }

            public void Reconnect()
            {
                throw new NotImplementedException();
            }

            public void Reconnect(DbConnection connection)
            {
                throw new NotImplementedException();
            }
#if !NET40 && !NET45
            DbConnection ISession.Close()
            {
                throw new NotImplementedException();
            }
#endif
            public void Reconnect(IDbConnection connection)
            {
                throw new NotImplementedException();
            }

            public IDbConnection Close()
            {
                throw new NotImplementedException();
            }

            public void CancelQuery()
            {
                throw new NotImplementedException();
            }

            public bool IsDirty()
            {
                throw new NotImplementedException();
            }

            public bool IsReadOnly(object entityOrProxy)
            {
                throw new NotImplementedException();
            }

            public void SetReadOnly(object entityOrProxy, bool readOnly)
            {
                throw new NotImplementedException();
            }

            public object GetIdentifier(object obj)
            {
                throw new NotImplementedException();
            }

            public bool Contains(object obj)
            {
                throw new NotImplementedException();
            }

            public void Evict(object obj)
            {
                throw new NotImplementedException();
            }

            public object Load(Type theType, object id, LockMode lockMode)
            {
                throw new NotImplementedException();
            }

            public object Load(string entityName, object id, LockMode lockMode)
            {
                throw new NotImplementedException();
            }

            public object Load(Type theType, object id)
            {
                throw new NotImplementedException();
            }

            public T Load<T>(object id, LockMode lockMode)
            {
                throw new NotImplementedException();
            }

            public T Load<T>(object id)
            {
                throw new NotImplementedException();
            }

            public object Load(string entityName, object id)
            {
                throw new NotImplementedException();
            }

            public void Load(object obj, object id)
            {
                throw new NotImplementedException();
            }

            public void Replicate(object obj, ReplicationMode replicationMode)
            {
                throw new NotImplementedException();
            }

            public void Replicate(string entityName, object obj, ReplicationMode replicationMode)
            {
                throw new NotImplementedException();
            }

            public object Save(object obj)
            {
                throw new NotImplementedException();
            }

            public void Save(object obj, object id)
            {
                throw new NotImplementedException();
            }

            public object Save(string entityName, object obj)
            {
                throw new NotImplementedException();
            }

            public void Save(string entityName, object obj, object id)
            {
                throw new NotImplementedException();
            }

            public void SaveOrUpdate(object obj)
            {
                throw new NotImplementedException();
            }

            public void SaveOrUpdate(string entityName, object obj)
            {
                throw new NotImplementedException();
            }

            public void SaveOrUpdate(string entityName, object obj, object id)
            {
                throw new NotImplementedException();
            }

            public void Update(object obj)
            {
                throw new NotImplementedException();
            }

            public void Update(object obj, object id)
            {
                throw new NotImplementedException();
            }

            public void Update(string entityName, object obj)
            {
                throw new NotImplementedException();
            }

            public void Update(string entityName, object obj, object id)
            {
                throw new NotImplementedException();
            }

            public object Merge(object obj)
            {
                throw new NotImplementedException();
            }

            public object Merge(string entityName, object obj)
            {
                throw new NotImplementedException();
            }

            public T Merge<T>(T entity) where T : class
            {
                throw new NotImplementedException();
            }

            public T Merge<T>(string entityName, T entity) where T : class
            {
                throw new NotImplementedException();
            }

            public void Persist(object obj)
            {
                throw new NotImplementedException();
            }

            public void Persist(string entityName, object obj)
            {
                throw new NotImplementedException();
            }

            public void Delete(object obj)
            {
                throw new NotImplementedException();
            }

            public void Delete(string entityName, object obj)
            {
                throw new NotImplementedException();
            }

            public int Delete(string query)
            {
                throw new NotImplementedException();
            }

            public int Delete(string query, object value, IType type)
            {
                throw new NotImplementedException();
            }

            public int Delete(string query, object[] values, IType[] types)
            {
                throw new NotImplementedException();
            }

            public void Lock(object obj, LockMode lockMode)
            {
                throw new NotImplementedException();
            }

            public void Lock(string entityName, object obj, LockMode lockMode)
            {
                throw new NotImplementedException();
            }

            public void Refresh(object obj)
            {
                throw new NotImplementedException();
            }

            public void Refresh(object obj, LockMode lockMode)
            {
                throw new NotImplementedException();
            }

            public LockMode GetCurrentLockMode(object obj)
            {
                throw new NotImplementedException();
            }

            public ITransaction BeginTransaction()
            {
                throw new NotImplementedException();
            }

            public ITransaction BeginTransaction(IsolationLevel isolationLevel)
            {
                // Simulating an exception which occurred in real life
                throw new TransactionException(@"
Begin failed with SQL exception
---> Npgsql.NpgsqlException (0x80004005): Exception while reading from stream
---> System.TimeoutException: Timeout during reading attempt
");            }

            public void JoinTransaction()
            {
                throw new NotImplementedException();
            }

            public ICriteria CreateCriteria<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public ICriteria CreateCriteria<T>(string alias) where T : class
            {
                throw new NotImplementedException();
            }

            public ICriteria CreateCriteria(Type persistentClass)
            {
                throw new NotImplementedException();
            }

            public ICriteria CreateCriteria(Type persistentClass, string alias)
            {
                throw new NotImplementedException();
            }

            public ICriteria CreateCriteria(string entityName)
            {
                throw new NotImplementedException();
            }

            public ICriteria CreateCriteria(string entityName, string alias)
            {
                throw new NotImplementedException();
            }

            public IQueryOver<T, T> QueryOver<T>() where T : class
            {
                throw new NotImplementedException();
            }

            public IQueryOver<T, T> QueryOver<T>(Expression<Func<T>> alias) where T : class
            {
                throw new NotImplementedException();
            }

            public IQueryOver<T, T> QueryOver<T>(string entityName) where T : class
            {
                throw new NotImplementedException();
            }

            public IQueryOver<T, T> QueryOver<T>(string entityName, Expression<Func<T>> alias) where T : class
            {
                throw new NotImplementedException();
            }

            public IQuery CreateQuery(string queryString)
            {
                throw new NotImplementedException();
            }

            public IQuery CreateFilter(object collection, string queryString)
            {
                throw new NotImplementedException();
            }

            public IQuery GetNamedQuery(string queryName)
            {
                throw new NotImplementedException();
            }

            public ISQLQuery CreateSQLQuery(string queryString)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public object Get(Type clazz, object id)
            {
                throw new NotImplementedException();
            }

            public object Get(Type clazz, object id, LockMode lockMode)
            {
                throw new NotImplementedException();
            }

            public object Get(string entityName, object id)
            {
                throw new NotImplementedException();
            }

            public T Get<T>(object id)
            {
                throw new NotImplementedException();
            }

            public T Get<T>(object id, LockMode lockMode)
            {
                throw new NotImplementedException();
            }

            public string GetEntityName(object obj)
            {
                throw new NotImplementedException();
            }

            public IFilter EnableFilter(string filterName)
            {
                throw new NotImplementedException();
            }

            public IFilter GetEnabledFilter(string filterName)
            {
                throw new NotImplementedException();
            }

            public void DisableFilter(string filterName)
            {
                throw new NotImplementedException();
            }

            public IMultiQuery CreateMultiQuery()
            {
                throw new NotImplementedException();
            }

            public ISession SetBatchSize(int batchSize)
            {
                throw new NotImplementedException();
            }

            public ISessionImplementor GetSessionImplementation()
            {
                throw new NotImplementedException();
            }

            public IMultiCriteria CreateMultiCriteria()
            {
                throw new NotImplementedException();
            }

            public ISession GetSession(EntityMode entityMode)
            {
                throw new NotImplementedException();
            }

            public IQueryable<T> Query<T>()
            {
                throw new NotImplementedException();
            }

            public IQueryable<T> Query<T>(string entityName)
            {
                throw new NotImplementedException();
            }

            public EntityMode ActiveEntityMode { get; }
            public FlushMode FlushMode { get; set; }
            public CacheMode CacheMode { get; set; }
            public ISessionFactory SessionFactory { get; }
#if !NET40 && !NET45
            DbConnection ISession.Connection => _;
#endif
            public IDbConnection Connection { get; }
            public bool IsOpen { get; }
            public bool IsConnected { get; }
            public bool DefaultReadOnly { get; set; }
            public ITransaction Transaction => new TestTransaction();
            public ISessionStatistics Statistics { get; }
        }

        private class TestSessionFactory : ISessionFactory
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public ISession OpenSession(IDbConnection conn)
            {
                return new TestSession();
            }

            public Task CloseAsync(CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictAsync(Type persistentClass, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictAsync(Type persistentClass, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictEntityAsync(string entityName, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictEntityAsync(string entityName, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictCollectionAsync(string roleName, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictCollectionAsync(string roleName, object id, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictQueriesAsync(CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task EvictQueriesAsync(string cacheRegion, CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

#if !NET40 && !NET45
public ISessionBuilder WithOptions()
            {
                throw new NotImplementedException();
            }
#endif
            public ISession OpenSession(DbConnection connection)
            {
                throw new NotImplementedException();
            }

            public ISession OpenSession(IInterceptor sessionLocalInterceptor)
            {
                throw new NotImplementedException();
            }

            public ISession OpenSession(DbConnection conn, IInterceptor sessionLocalInterceptor)
            {
                throw new NotImplementedException();
            }

            public ISession OpenSession(IDbConnection conn, IInterceptor sessionLocalInterceptor)
            {
                throw new NotImplementedException();
            }

            public ISession OpenSession()
            {
                return new TestSession();
            }
#if !NET40 && !NET45
            public IStatelessSessionBuilder WithStatelessOptions()
            {
                throw new NotImplementedException();
            }
#endif
            public IStatelessSession OpenStatelessSession(DbConnection connection)
            {
                throw new NotImplementedException();
            }

            public IClassMetadata GetClassMetadata(Type persistentClass)
            {
                throw new NotImplementedException();
            }

            public IClassMetadata GetClassMetadata(string entityName)
            {
                throw new NotImplementedException();
            }

            public ICollectionMetadata GetCollectionMetadata(string roleName)
            {
                throw new NotImplementedException();
            }

            public IDictionary<string, IClassMetadata> GetAllClassMetadata()
            {
                throw new NotImplementedException();
            }

            public IDictionary<string, ICollectionMetadata> GetAllCollectionMetadata()
            {
                throw new NotImplementedException();
            }

            public void Close()
            {
                throw new NotImplementedException();
            }

            public void Evict(Type persistentClass)
            {
                throw new NotImplementedException();
            }

            public void Evict(Type persistentClass, object id)
            {
                throw new NotImplementedException();
            }

            public void EvictEntity(string entityName)
            {
                throw new NotImplementedException();
            }

            public void EvictEntity(string entityName, object id)
            {
                throw new NotImplementedException();
            }

            public void EvictCollection(string roleName)
            {
                throw new NotImplementedException();
            }

            public void EvictCollection(string roleName, object id)
            {
                throw new NotImplementedException();
            }

            public void EvictQueries()
            {
                throw new NotImplementedException();
            }

            public void EvictQueries(string cacheRegion)
            {
                throw new NotImplementedException();
            }

            public IStatelessSession OpenStatelessSession()
            {
                throw new NotImplementedException();
            }

            public IStatelessSession OpenStatelessSession(IDbConnection connection)
            {
                throw new NotImplementedException();
            }

            public FilterDefinition GetFilterDefinition(string filterName)
            {
                throw new NotImplementedException();
            }

            public ISession GetCurrentSession()
            {
                throw new NotImplementedException();
            }

            public IStatistics Statistics { get; }
            public bool IsClosed { get; }
            public ICollection<string> DefinedFilterNames { get; }
        }

        private class TestTransaction : ITransaction
        {
            public void Dispose()
            {
            }

            public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public Task RollbackAsync(CancellationToken cancellationToken = new CancellationToken())
            {
                throw new NotImplementedException();
            }

            public void Begin()
            {
                throw new NotImplementedException();
            }

            public void Begin(IsolationLevel isolationLevel)
            {
                throw new NotImplementedException();
            }

            public void Commit()
            {
                // Simulating an exception which occurred in real life
                throw new TransactionException("Transaction not successfully started");
            }

            public void Rollback()
            {
            }

            public void Enlist(IDbCommand command)
            {
                throw new NotImplementedException();
            }

            public void Enlist(DbCommand command)
            {
                throw new NotImplementedException();
            }

            public void RegisterSynchronization(ISynchronization synchronization)
            {
                throw new NotImplementedException();
            }

            public bool IsActive { get; }
            public bool WasRolledBack { get; }
            public bool WasCommitted { get; }
        }
        #endregion
    }
}