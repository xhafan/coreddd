using System.Transactions;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.Committing
{
    [TestFixture]
    public class when_rolling_back_unit_of_work_after_committing_it_within_transaction_scope
    {
        private NhibernateUnitOfWork _unitOfWork;
        private NhibernateRepository<TestEntity> _testEntityRepository;
        private TestEntity _testEntity;

        [SetUp]
        public void Context()
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted}))
            {
                _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
                _unitOfWork.BeginTransaction();

                _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);
                _testEntity = new TestEntity();
                _testEntityRepository.Save(_testEntity);

                _unitOfWork.Commit();

                transactionScope.Complete();
            }

            _unitOfWork.Rollback();
        }

        [Test]
        public void entities_are_persisted_and_rollback_is_handled_gracefully()
        {
            _unitOfWork.BeginTransaction();
            _testEntity = _testEntityRepository.Get(_testEntity.Id);

            _testEntity.ShouldNotBeNull();

            _unitOfWork.Rollback();
        }
    }
}