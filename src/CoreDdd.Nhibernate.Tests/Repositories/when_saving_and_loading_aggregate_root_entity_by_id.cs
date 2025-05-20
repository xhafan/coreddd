using CoreDdd.Nhibernate.Repositories;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories;

[TestFixture]
public class when_saving_and_loading_aggregate_root_entity_by_id : BasePersistenceTest
{
    [Test]
    public void entity_is_persisted_and_retrieved()
    {
        var testEntityRepository = new NhibernateRepository<TestEntity>(UnitOfWork);
        var testEntity = new TestEntity();


        testEntityRepository.Save(testEntity);


        UnitOfWork.Flush();
        UnitOfWork.Clear();
        testEntity = testEntityRepository.LoadById(testEntity.Id);

        testEntity.ShouldNotBeNull();            
    }
}