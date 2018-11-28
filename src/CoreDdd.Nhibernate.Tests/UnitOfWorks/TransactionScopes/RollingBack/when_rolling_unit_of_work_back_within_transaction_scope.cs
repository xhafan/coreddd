using System.Transactions;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.RollingBack
{
    [TestFixture(TypeArgs = new[] { typeof(RollingBackUnitOfWorkInTransactionScopeSpecification) })]
#if !NET40 && !NET45
    [TestFixture(TypeArgs = new[] { typeof(RollingBackAsyncUnitOfWorkInTransactionScopeSpecification) })]
#endif
    public class when_rolling_unit_of_work_back_within_transaction_scope<TRollingBackUnitOfWorkInTransactionScopeSpecification>
        where TRollingBackUnitOfWorkInTransactionScopeSpecification : IRollingBackUnitOfWorkInTransactionScopeSpecification, new()
    {
        private NhibernateUnitOfWork _unitOfWork;
        private NhibernateRepository<TestEntity> _testEntityRepository;
        private TestEntity _testEntity;

        [SetUp]
        public void Context()
        {
            var specification = new TRollingBackUnitOfWorkInTransactionScopeSpecification();

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
                _unitOfWork.BeginTransaction();

                _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);
                _testEntity = new TestEntity();
                _testEntityRepository.Save(_testEntity);

                specification.RollbackAct(_unitOfWork);
            }
        }

        [Test]
        public void entities_are_not_persisted()
        {
            if (!_shouldBeTestedDueToOldNhibernateVersionAndSqliteDriver()) return;

            _unitOfWork.BeginTransaction();
            _testEntity = _testEntityRepository.Get(_testEntity.Id);

            _testEntity.ShouldBeNull();

            _unitOfWork.Rollback();

            bool _shouldBeTestedDueToOldNhibernateVersionAndSqliteDriver()
            {
                // NHibernate 4.1.1 does not rollback properly within a transaction scope for sqllite (latest stable 1.0.108). This is working fine for NHibernate 5.0.3
                // strangely, if there is a TestEntity sql select just before the TestEntity sql insert, it all works fine. Anyway, marking it as not working for 
                // NHibernate 4.1.1 so it's visible that there might be some issues with transaction scope, NHibernate 4.1.1 and sqllite combination.

                var configuration = IoC.Resolve<INhibernateConfigurator>().GetConfiguration();
                var connectionDriverClass = configuration.Properties["connection.driver_class"];
                var isSqlite = connectionDriverClass.Contains("SQLite");

                if (isSqlite)
                {
#if NET40 // NHibernate 4.1.1
                    return false;
#endif
#if NET45 // NHibernate 4.1.1
                    return false;
#endif
#if NET461
                    return true;
#endif                    
                }

                return true;
            }
        }

        [Test]
        public void nhibernate_session_is_closed()
        {
            _unitOfWork.Session.ShouldBeNull();            
        }
    }
}