#if !NET40 && !NET45
using System.Threading.Tasks;
using CoreDdd.Nhibernate.Repositories;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories;

[TestFixture]
public class when_saving_and_loading_aggregate_root_entity_by_id_async : BasePersistenceTest
{
    [Test]
    public async Task entity_is_persisted_and_retrieved()
    {
        var testEntityRepository = new NhibernateRepository<TestEntity>(UnitOfWork);
        var testEntity = new TestEntity();


        await testEntityRepository.SaveAsync(testEntity);


        await UnitOfWork.FlushAsync();
        UnitOfWork.Clear();
        testEntity = await testEntityRepository.LoadByIdAsync(testEntity.Id);

        testEntity.ShouldNotBeNull();            
    }
}
#endif