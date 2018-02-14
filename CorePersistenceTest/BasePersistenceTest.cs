using CoreDdd.Domain;
using CoreDdd.UnitOfWorks;
using CoreUtils.Extensions;
using NUnit.Framework;

namespace CorePersistenceTest
{
    public abstract class BasePersistenceTest
    {
        protected IUnitOfWork UnitOfWork;

        protected abstract void Context();

        protected abstract IUnitOfWork ResolveUnitOfWork();
        protected abstract void GetSessionFromUnitOfWork();

        [SetUp]
        public void TestFixtureSetUp()
        {
            UnitOfWork = ResolveUnitOfWork();
            UnitOfWork.BeginTransaction();
            GetSessionFromUnitOfWork();

            Context();
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            UnitOfWork.Rollback();
        }

        protected void Save(params IAggregateRoot[] aggregateRoots)
        {
            aggregateRoots.Each(SaveOrUpdate);
            Flush();
        }

        protected abstract void SaveOrUpdate(object entity);
        protected abstract void Flush();
        protected abstract TAggregateRoot Get<TAggregateRoot>(int id) where TAggregateRoot : IAggregateRoot;
        protected abstract void Delete(string query);
    }
}