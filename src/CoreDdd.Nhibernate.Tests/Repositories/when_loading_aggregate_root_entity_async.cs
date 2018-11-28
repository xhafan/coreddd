#if !NET40 && !NET45
using System.Threading.Tasks;
using CoreDdd.Nhibernate.Repositories;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_loading_aggregate_root_entity_async : BasePersistenceTest
    {
        [Test]
        public async Task entity_is_loaded_from_database()
        {
            var entityRepository = new NhibernateRepository<EntityWithText>(UnitOfWork);
            var entity = new EntityWithText("hello");
            await entityRepository.SaveAsync(entity);
            UnitOfWork.Flush();
            UnitOfWork.Clear();


            entity = await entityRepository.LoadAsync(entity.Id);


            entity.ShouldNotBeNull();            
            entity.Text.ShouldBe("hello");            
        }
    }
}
#endif