using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_deleting_aggregate_root_entity : BasePersistenceTest
    {
        [Test]
        public void entity_is_deleted()
        {
            var testEntityRepository = new NhibernateRepository<TestEntity>(UnitOfWork);
            var testEntity = new TestEntity();
            testEntityRepository.Save(testEntity);
            UnitOfWork.Flush();
            UnitOfWork.Clear();


            testEntityRepository.Delete(testEntity);
            UnitOfWork.Flush();
            UnitOfWork.Clear();


            testEntity = testEntityRepository.Get(testEntity.Id);

            testEntity.ShouldBeNull();            
        }
    }
}