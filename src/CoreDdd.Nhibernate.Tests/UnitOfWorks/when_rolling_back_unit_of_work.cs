using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_rolling_back_unit_of_work
    {
        private NhibernateUnitOfWork _unitOfWork;
        private TestEntity _testEntity;
        private NhibernateRepository<TestEntity> _testEntityRepository;

        [SetUp]
        public void Context()
        {
            _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction();

            _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);

            _testEntity = new TestEntity();
            _testEntityRepository.Save(_testEntity);
            _unitOfWork.Flush();

            _unitOfWork.Rollback();
        }

        [Test]
        public void entities_are_not_persisted()
        {
            _unitOfWork.BeginTransaction();
            _testEntity = _testEntityRepository.Get(_testEntity.Id);

            _testEntity.ShouldBeNull();

            _unitOfWork.Rollback();
        }

        [Test]
        public void nhibernate_session_is_closed()
        {
            _unitOfWork.Session.ShouldBeNull();            
        }
    }
}