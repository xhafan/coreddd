#if !NET40 && !NET45
using System.Threading.Tasks;
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
    public class when_loading_aggregate_root_entity_async : BasePersistenceTest
    {
        [Test]
        public async Task entity_is_loaded_from_database()
        {
            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            var entityRepository = new NhibernateRepository<EntityWithText>(unitOfWork);
            var entity = new EntityWithText("hello");
            await entityRepository.SaveAsync(entity);
            unitOfWork.Flush();
            unitOfWork.Clear();


            entity = await entityRepository.LoadAsync(entity.Id);


            entity.ShouldNotBeNull();            
            entity.Text.ShouldBe("hello");            
        }
    }
}
#endif