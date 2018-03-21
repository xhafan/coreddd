using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests.UnitOfWorks
{
    [TestFixture]
    public class when_commiting_unit_of_work
    {
        private NhibernateUnitOfWork _unitOfWork;

        [Test]
        public void entities_stays_persisted()
        {
            _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction();

            var testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);
       
            var testEntity = new TestEntity();
            testEntityRepository.Save(testEntity);

            _unitOfWork.Commit();

            _unitOfWork.BeginTransaction();
            testEntity = testEntityRepository.Get(testEntity.Id);

            testEntity.ShouldNotBeNull();

            _unitOfWork.Rollback();
        }
    }
}