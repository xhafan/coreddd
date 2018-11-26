#if !NET40 && !NET45 && !NET451
using System.Threading.Tasks;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_deleting_aggregate_root_entity_async : BasePersistenceTest
    {
        [Test]
        public async Task entity_is_deleted()
        {
            var testEntityRepository = new NhibernateRepository<TestEntity>(UnitOfWork);
            var testEntity = new TestEntity();
            await testEntityRepository.SaveAsync(testEntity);
            UnitOfWork.Flush();
            UnitOfWork.Clear();


            await testEntityRepository.DeleteAsync(testEntity);
            UnitOfWork.Flush();
            UnitOfWork.Clear();


            testEntity = await testEntityRepository.GetAsync(testEntity.Id);

            testEntity.ShouldBeNull();            
        }
    }
}
#endif