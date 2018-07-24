#if !NET40 && !NET45
using System.Threading.Tasks;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_flushing_unit_of_work_async
    {
        [Test]
        public async Task entities_are_persisted()
        {
            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            unitOfWork.BeginTransaction();
            var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);
            var testEntity = new TestEntity();
            testEntityRepository.Save(testEntity);


            await unitOfWork.FlushAsync();


            unitOfWork.Clear();
            testEntity = testEntityRepository.Get(testEntity.Id);

            testEntity.ShouldNotBeNull();

            unitOfWork.Rollback();
        }
    }
}
#endif