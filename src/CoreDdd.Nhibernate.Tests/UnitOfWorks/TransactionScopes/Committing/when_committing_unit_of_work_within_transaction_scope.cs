using System.Transactions;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes.Committing
{
    [TestFixture(TypeArgs = new[] { typeof(CommittingUnitOfWorkInTransactionScopeSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(DisposingUnitOfWorkInTransactionScopeSpecification) })]
#if !NET40 && !NET45
    [TestFixture(TypeArgs = new[] { typeof(CommittingAsyncUnitOfWorkInTransactionScopeSpecification) })]
#endif
    public class when_committing_unit_of_work_within_transaction_scope<TCommittingUnitOfWorkInTransactionScopeSpecification>
        where TCommittingUnitOfWorkInTransactionScopeSpecification : ICommittingUnitOfWorkInTransactionScopeSpecification, new()
    {
        private NhibernateUnitOfWork _unitOfWork;
        private NhibernateRepository<TestEntity> _testEntityRepository;
        private TestEntity _testEntity;

        [SetUp]
        public void Context()
        {
            var specification = new TCommittingUnitOfWorkInTransactionScopeSpecification();

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted}))
            {
                _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
                _unitOfWork.BeginTransaction();

                _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);
                _testEntity = new TestEntity();
                _testEntityRepository.Save(_testEntity);

                specification.CommitAct(_unitOfWork);

                transactionScope.Complete();
            }
        }

        [Test]
        public void entities_are_persisted()
        {
            _unitOfWork.BeginTransaction();
            _testEntity = _testEntityRepository.Get(_testEntity.Id);

            _testEntity.ShouldNotBeNull();

            _unitOfWork.Rollback();
        }

        [Test]
        public void nhibernate_session_is_closed()
        {
            _unitOfWork.Session.ShouldBeNull();
        }
    }
}