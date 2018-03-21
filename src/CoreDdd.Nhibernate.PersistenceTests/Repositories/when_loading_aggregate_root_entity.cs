using CoreDdd.Nhibernate.PersistenceTests.TestEntities;
using CoreDdd.Nhibernate.PersistenceTests.UnitOfWorks;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.PersistenceTests.Repositories
{
    [TestFixture]
    public class when_loading_aggregate_root_entity : BasePersistenceTest
    {
        [Test]
        public void entity_is_loaded_from_database()
        {
            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            var entityRepository = new NhibernateRepository<EntityWithText>(unitOfWork);
            var entity = new EntityWithText("hello");
            entityRepository.Save(entity);            
            unitOfWork.Flush();
            unitOfWork.Clear();


            entity = entityRepository.Load(entity.Id);


            entity.ShouldNotBeNull();            
            entity.Text.ShouldBe("hello");            
        }
    }
}