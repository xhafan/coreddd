using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests.UnitOfWorks
{
    [TestFixture]
    public class when_rolling_back_unit_of_work
    {
        private NhibernateUnitOfWork _unitOfWork;

        [Test]
        public void entities_are_not_persisted()
        {
            _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction();

            var testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);
       
            var testEntity = new TestEntity();
            testEntityRepository.Save(testEntity);

            _unitOfWork.Rollback();

            _unitOfWork.BeginTransaction();
            testEntity = testEntityRepository.Get(testEntity.Id);

            testEntity.ShouldBeNull();

            _unitOfWork.Rollback();
        }
    }
}