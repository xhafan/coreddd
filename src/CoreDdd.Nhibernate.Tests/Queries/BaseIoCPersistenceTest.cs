using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    public abstract class BaseIoCPersistenceTest
    { 
        protected NhibernateUnitOfWork UnitOfWork;

        protected BaseIoCPersistenceTest()
        {
            UnitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
        }

        [SetUp]
        public void TestFixtureSetUp()
        {
            UnitOfWork.BeginTransaction();
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            UnitOfWork.Rollback();
        }
    }
}