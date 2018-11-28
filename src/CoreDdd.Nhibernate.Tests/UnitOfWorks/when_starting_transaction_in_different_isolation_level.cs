using System.Data;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_starting_transaction_in_different_isolation_level
    {
        private NhibernateUnitOfWork _unitOfWork;
        private TestEntity _testEntity;
        private NhibernateRepository<TestEntity> _testEntityRepository;

        [SetUp]
        public void Context()
        {
            _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction(IsolationLevel.Serializable);

            _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);

            _testEntity = new TestEntity();
            _testEntityRepository.Save(_testEntity);

            _unitOfWork.Commit();
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