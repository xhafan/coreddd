using DddCore;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Utilities.Extensions;
using FluentNHibernate;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public abstract class PersistenceTestBase
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
                Session.Delete("from EmailTemplate");
                tx.Commit();
            }
        }

        private void _ConfigureNHibernate()
        {
#if(DEBUG)
            NHibernateProfiler.Initialize();
#endif
            var configuration = new Configuration();
            configuration.Configure();
            var persistenceModel = new PersistenceModel();
            persistenceModel.AddMappingsFromAssembly(typeof(EmailTemplate).Assembly);
            persistenceModel.Configure(configuration);
            var sessionFactory = configuration.BuildSessionFactory();
            Session = sessionFactory.OpenSession();
        }
    }
}