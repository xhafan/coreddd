using CoreDdd.Nhibernate.UnitOfWorks;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests
{
    public abstract class BasePersistenceTest
    {
        protected NhibernateUnitOfWork UnitOfWork;

        protected BasePersistenceTest()
        {
            UnitOfWork = new NhibernateUnitOfWork(new TestNhibernateConfigurator());
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