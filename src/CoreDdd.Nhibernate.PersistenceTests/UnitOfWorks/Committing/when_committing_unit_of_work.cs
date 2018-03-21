using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests.UnitOfWorks.Committing
{
    [TestFixture(TypeArgs = new[] { typeof(CommittingUnitOfWorkSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(DisposingUnitOfWorkSpecification) })]
    public class when_committing_unit_of_work<TCommittingUnitOfWorkSpecification>
        where TCommittingUnitOfWorkSpecification : ICommittingUnitOfWorkSpecification, new()
    {
        private NhibernateUnitOfWork _unitOfWork;
        private NhibernateRepository<TestEntity> _testEntityRepository;
        private TestEntity _testEntity;

        [SetUp]
        public void Context()
        {
            var specification = new TCommittingUnitOfWorkSpecification();

            _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction();

            _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);

            _testEntity = new TestEntity();
            _testEntityRepository.Save(_testEntity);

            specification.CommitAct(_unitOfWork);
        }

        [Test]
        public void entities_stays_persisted()
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

        [Test]
        public void calling_dispose_after_commit_is_safe()
        {
            _unitOfWork.Dispose();
        }
    }
}