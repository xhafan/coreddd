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
    public class when_deleting_aggregate_root_entity_async : BasePersistenceTest
    {
        [Test]
        public async Task entity_is_deleted()
        {
            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);
            var testEntity = new TestEntity();
            await testEntityRepository.SaveAsync(testEntity);            
            unitOfWork.Flush();
            unitOfWork.Clear();


            await testEntityRepository.DeleteAsync(testEntity);
            unitOfWork.Flush();
            unitOfWork.Clear();


            testEntity = await testEntityRepository.GetAsync(testEntity.Id);

            testEntity.ShouldBeNull();            
        }
    }
}
#endif