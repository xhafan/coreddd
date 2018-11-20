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

        protected void Save<TAggregateRoot>(TAggregateRoot aggregateRoot) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            PersistenceTestHelper.Save(aggregateRoot);
        }

        protected void SaveGeneric<TAggregateRoot, TId>(TAggregateRoot aggregateRoot) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            PersistenceTestHelper.SaveGeneric<TAggregateRoot, TId>(aggregateRoot);
        }

        protected TAggregateRoot Get<TAggregateRoot>(int id) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            return PersistenceTestHelper.Get<TAggregateRoot>(id);
        }

        protected TAggregateRoot GetGeneric<TAggregateRoot, TId>(TId id) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            return PersistenceTestHelper.GetGeneric<TAggregateRoot, TId>(id);
        }

        protected TAggregateRoot Load<TAggregateRoot>(int id) 
            where TAggregateRoot : Entity, IAggregateRoot
        {
            return PersistenceTestHelper.Load<TAggregateRoot>(id);
        }

        protected TAggregateRoot LoadGeneric<TAggregateRoot, TId>(TId id) 
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            return PersistenceTestHelper.LoadGeneric<TAggregateRoot, TId>(id);
        }

        protected void Delete<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : Entity, IAggregateRoot
        {
            PersistenceTestHelper.Delete(aggregateRoot);
        }

        protected void DeleteGeneric<TAggregateRoot, TId>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : Entity<TId>, IAggregateRoot
        {
            PersistenceTestHelper.DeleteGeneric<TAggregateRoot, TId>(aggregateRoot);
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