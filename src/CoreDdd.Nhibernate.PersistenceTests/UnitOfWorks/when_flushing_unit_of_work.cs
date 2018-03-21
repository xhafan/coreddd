using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests.UnitOfWorks
{
    [TestFixture]
    public class when_flushing_unit_of_work
    {
        [Test]
        public void entities_are_persisted()
        {
            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            unitOfWork.BeginTransaction();
            var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);
            var testEntity = new TestEntity();
            testEntityRepository.Save(testEntity);


            unitOfWork.Flush();


            unitOfWork.Clear();
            testEntity = testEntityRepository.Get(testEntity.Id);

            testEntity.ShouldNotBeNull();

            unitOfWork.Rollback();
        }
    }
}