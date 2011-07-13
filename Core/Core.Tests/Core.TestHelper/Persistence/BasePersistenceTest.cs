using Core.Domain;
using Core.Utilities.Extensions;
using Core.Utilities.NHibernate;
using NHibernate;
using NUnit.Framework;

namespace Core.TestHelper.Persistence
{
    public abstract class BasePersistenceTest
    {
        protected ISession Session;

        public abstract void Context();
     
        [TestFixtureSetUp]
        public void SetUp()
        {
            _ConfigureNHibernate();

            _ClearDatabase();

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

        private void _ClearDatabase()
        {
            using (var tx = Session.BeginTransaction())
            {
                Session.Delete("from Email");
                Session.Delete("from EmailTemplate");
                Session.Delete("from Recipient");
                tx.Commit();
            }
        }

        private void _ConfigureNHibernate()
        {
            var sessionFactory = NHibernateUtilities.ConfigureNHibernate();
            Session = sessionFactory.OpenSession();
        }
    }
}