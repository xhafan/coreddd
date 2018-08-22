#if !NET40 && !NET45 && !NET451
using System.Threading.Tasks;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_saving_and_getting_aggregate_root_entity_async : BasePersistenceTest
    {
        [Test]
        public async Task entity_is_persisted_and_retrieved()
        {
            var testEntityRepository = new NhibernateRepository<TestEntity>(UnitOfWork);
            var testEntity = new TestEntity();


            testEntityRepository.Save(testEntity);


            UnitOfWork.Flush();
            UnitOfWork.Clear();
            testEntity = await testEntityRepository.GetAsync(testEntity.Id);

            testEntity.ShouldNotBeNull();            
        }
    }
}
#endif