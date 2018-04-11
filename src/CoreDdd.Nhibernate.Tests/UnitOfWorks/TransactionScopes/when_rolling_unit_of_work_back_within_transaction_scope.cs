using System.Transactions;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes
{
    [TestFixture]
    public class when_rolling_unit_of_work_back_within_transaction_scope
    {
        private NhibernateUnitOfWork _unitOfWork;
        private NhibernateRepository<TestEntity> _testEntityRepository;
        private TestEntity _testEntity;

        [SetUp]
        public void Context()
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
                _unitOfWork.BeginTransaction();

                _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);
                _testEntity = new TestEntity();
                _testEntityRepository.Save(_testEntity);

                _unitOfWork.Rollback();
            }
        }

        [Test]
        public void entities_are_not_persisted()
        {
            if (_shouldNotBeTestedDueToOldNhibernateVersionAndSqliteDriver()) return;

            _unitOfWork.BeginTransaction();
            _testEntity = _testEntityRepository.Get(_testEntity.Id);

            _testEntity.ShouldBeNull();

            _unitOfWork.Rollback();

            bool _shouldNotBeTestedDueToOldNhibernateVersionAndSqliteDriver()
            {
                // Nhibernate 4.1.1 does not rollback properly within a transaction scope for sqllite (latest stable 1.0.108). This is working fine for Nhibernate 5.0.3
                // strangely, if there is a TestEntity sql select just before the TestEntity sql insert, it all works fine. Anyway, marking it as not working for 
                // Nhibernate 4.1.1 so it's visible that there might be some issues with transaction scope, nhibernate 4.1.1 and sqlite combination.

                var configuration = IoC.Resolve<INhibernateConfigurator>().GetConfiguration();
                var connectionDriverClass = configuration.Properties["connection.driver_class"];
                var isSqlite = connectionDriverClass.Contains("SQLite");

                if (isSqlite)
                {
#if NET40 //Nhibernate 4.1.1
                    return true;
#endif
#if NET45 //Nhibernate 4.1.1
                    return true;
#endif
#if NET461
                    return false;
#endif                    
                }

                return false;
            }
        }

        [Test]
        public void nhibernate_session_is_closed()
        {
            _unitOfWork.Session.ShouldBeNull();            
        }
    }
}