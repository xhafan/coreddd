using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_saving_and_getting_aggregate_root_entity : BasePersistenceTest
    {
        [Test]
        public void entity_is_persisted()
        {
            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);
            var testEntity = new TestEntity();


            testEntityRepository.Save(testEntity);

            
            unitOfWork.Flush();
            unitOfWork.Clear();
            testEntity = testEntityRepository.Get(testEntity.Id);

            testEntity.ShouldNotBeNull();            
        }
    }
}