using CoreDdd.Domain;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.TestHelpers
{
    public abstract class BasePersistenceTest
    {
        protected PersistenceTestHelper PersistenceTestHelper;

        [SetUp]
        public void TestFixtureSetUp()
        {
            PersistenceTestHelper = new PersistenceTestHelper(IoC.Resolve<NhibernateUnitOfWork>());
            PersistenceTestHelper.BeginTransaction();
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            PersistenceTestHelper.Rollback();
        }

        protected void Save<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : IAggregateRoot
        {
            PersistenceTestHelper.Save(entity);
        }

        protected void SaveGeneric<TAggregateRoot, TId>(TAggregateRoot entity) where TAggregateRoot : IAggregateRoot
        {
            PersistenceTestHelper.SaveGeneric<TAggregateRoot, TId>(entity);
        }

        protected TAggregateRoot Get<TAggregateRoot>(int id) where TAggregateRoot : IAggregateRoot
        {
            return PersistenceTestHelper.Get<TAggregateRoot>(id);
        }

        protected TAggregateRoot GetGeneric<TAggregateRoot, TId>(TId id) where TAggregateRoot : IAggregateRoot
        {
            return PersistenceTestHelper.GetGeneric<TAggregateRoot, TId>(id);
        }

        protected TAggregateRoot Load<TAggregateRoot>(int id) where TAggregateRoot : IAggregateRoot
        {
            return PersistenceTestHelper.Load<TAggregateRoot>(id);
        }

        protected TAggregateRoot LoadGeneric<TAggregateRoot, TId>(TId id) where TAggregateRoot : IAggregateRoot
        {
            return PersistenceTestHelper.LoadGeneric<TAggregateRoot, TId>(id);
        }

        protected void Flush()
        {
            PersistenceTestHelper.UnitOfWork.Flush();
        }

        protected void Clear()
        {
            PersistenceTestHelper.UnitOfWork.Clear();
        }
    }
}