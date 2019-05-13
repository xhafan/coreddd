using CoreDdd.Nhibernate.Repositories;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NHibernate;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_saving_and_getting_aggregate_root_entity_using_a_lock : BasePersistenceTest
    {
        [Test]
        public void entity_is_persisted_and_retrieved()
        {
            var testEntityRepository = new NhibernateRepository<TestEntity>(UnitOfWork);
            var testEntity = new TestEntity();


            testEntityRepository.Save(testEntity);


            UnitOfWork.Flush();
            UnitOfWork.Clear();
            testEntity = testEntityRepository.Get(testEntity.Id, LockMode.Upgrade);

            testEntity.ShouldNotBeNull();            
        }
    }
}