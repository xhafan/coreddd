using Core.Domain;
using Core.Utilities.Extensions;
using NHibernate;
using NUnit.Framework;

namespace Core.Tests.Helpers.Persistence
{
    public abstract class BasePersistenceTest
    {
        protected ISession Session;

        protected abstract void ConfigureNHibernate();
        protected abstract void ClearDatabase();
        protected abstract void Context();

        [TestFixtureSetUp]
        public void SetUp()
        {
            ConfigureNHibernate();
            ClearDatabase();
            Context();
        }

        protected void Save(params IAggregateRootEntity[] aggregateRootEntitites)
        {
            aggregateRootEntitites.Each(e => Session.SaveOrUpdate(e));
            Session.Flush();
        }

        protected TAggregateRootEntity Get<TAggregateRootEntity>(int id) where TAggregateRootEntity : IAggregateRootEntity
        {
            return Session.Get<TAggregateRootEntity>(id);
        }
    }
}