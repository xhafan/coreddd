using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_loading_aggregate_root_entity : BasePersistenceTest
    {
        [Test]
        public void entity_is_loaded_from_database()
        {
            var entityRepository = new NhibernateRepository<EntityWithText>(UnitOfWork);
            var entity = new EntityWithText("hello");
            entityRepository.Save(entity);
            UnitOfWork.Flush();
            UnitOfWork.Clear();


            entity = entityRepository.Load(entity.Id);


            entity.ShouldNotBeNull();            
            entity.Text.ShouldBe("hello");            
        }
    }
}