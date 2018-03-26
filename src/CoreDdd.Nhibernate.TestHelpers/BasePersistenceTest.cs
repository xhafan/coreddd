using CoreDdd.Domain;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.TestHelpers
{
    public abstract class BasePersistenceTest
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

        protected void Save<TAggregateRoot>(TAggregateRoot entity) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            repository.Save(entity);
            Flush();
        }

        protected void SaveGeneric<TAggregateRoot, TId>(TAggregateRoot entity) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            repository.Save(entity);
            Flush();
        }

        protected TAggregateRoot Get<TAggregateRoot>(int id) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            return repository.Get(id);
        }

        protected TAggregateRoot GetGeneric<TAggregateRoot, TId>(TId id) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot, TId>(UnitOfWork);
            return repository.Get(id);
        }

        protected TAggregateRoot Load<TAggregateRoot>(int id) where TAggregateRoot : IAggregateRoot
        {
            var repository = new NhibernateRepository<TAggregateRoot>(UnitOfWork);
            return repository.Load(id);
        }

        protected void Flush()
        {
            UnitOfWork.Flush();
        }

        protected void Clear()
        {
            UnitOfWork.Clear();
        }
    }
}