using System.Transactions;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.TransactionScopes
{
    [TestFixture]
    public class when_committing_unit_of_work_within_transaction_scope
    {
        private NhibernateUnitOfWork _unitOfWork;
        private NhibernateRepository<TestEntity> _testEntityRepository;
        private TestEntity _testEntity;

        [SetUp]
        public void Context()
        {
            using (var transactionScope = new TransactionScope())
            {
                _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
                _unitOfWork.BeginTransaction();

                _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);
                _testEntity = new TestEntity();
                _testEntityRepository.Save(_testEntity);

                _unitOfWork.Commit();

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
    }
}