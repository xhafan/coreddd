using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.TestHelpers
{
    public abstract class BasePersistenceTest2
    {
        protected NhibernateUnitOfWork UnitOfWork;

        [SetUp]
        public void TestFixtureSetUp()
        {
            UnitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            UnitOfWork.BeginTransaction();
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            UnitOfWork.Rollback();
        }
    }
}