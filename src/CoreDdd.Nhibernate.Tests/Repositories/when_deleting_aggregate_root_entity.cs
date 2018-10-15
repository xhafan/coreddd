using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.TestHelpers;
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
            var testEntityRepository = new NhibernateRepository<TestEntity>(PersistenceTestHelper.UnitOfWork);
            var testEntity = new TestEntity();
            testEntityRepository.Save(testEntity);
            Flush();
            Clear();


            testEntityRepository.Delete(testEntity);
            Flush();
            Clear();


            testEntity = testEntityRepository.Get(testEntity.Id);

            testEntity.ShouldBeNull();            
        }
    }
}