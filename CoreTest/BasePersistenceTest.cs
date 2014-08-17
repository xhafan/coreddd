using System.Linq;
using CoreDdd.Domain;
using CoreDdd.UnitOfWorks;
using CoreUtils;
using CoreUtils.Extensions;
using NUnit.Framework;

namespace CoreTest
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

            ClearDatabase();
            Context();
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            UnitOfWork.Commit();
        }

        protected abstract IAggregateRootTypesToClearProvider GetAggregateRootTypesToClearProvider();

        protected void ClearDatabase()
        {
            GetAggregateRootTypesToClearProvider().GetAggregateRootTypesToClear().Each(x =>
                {
                    var typeName = x.Name;
                    var isAggregateRoot = x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IAggregateRoot<>));
                    Guard.Hope(isAggregateRoot, typeName + " is not implementing " + typeof(IAggregateRoot).Name);
                    Delete("from " + typeName);
                });
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