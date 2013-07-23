using System.Linq;
using CoreDdd.Domain;
using CoreDdd.Infrastructure;
using CoreUtils;
using CoreUtils.Extensions;
using NHibernate;
using NUnit.Framework;

namespace CoreTest
{
    public abstract class BasePersistenceTest
    {
        protected ISession Session;

        protected abstract void Context();

        [TestFixtureSetUp]
        public void SetUp()
        {
            Session = UnitOfWork.CurrentSession;
            ClearDatabase();
            Context();
        }

        protected abstract IAggregateRootTypesToClearProvider GetAggregateRootTypesToClearProvider();

        protected void ClearDatabase()
        {
            using (var tx = Session.BeginTransaction())
            {
                GetAggregateRootTypesToClearProvider().GetAggregateRootTypesToClear().Each(x =>
                    {
                        var typeName = x.Name;
                        var isAggregateRoot = x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IAggregateRoot<>));
                        Guard.Hope(isAggregateRoot, typeName + " is not implementing " + typeof(IAggregateRoot).Name);
                        Session.Delete("from " + typeName);
                    });
                tx.Commit();
            }  
        }

        protected void Save(params IAggregateRoot[] aggregateRoots)
        {
            aggregateRoots.Each(e => Session.SaveOrUpdate(e));
            Session.Flush();
        }

        protected TAggregateRoot Get<TAggregateRoot>(int id) where TAggregateRoot : IAggregateRoot
        {
            return Session.Get<TAggregateRoot>(id);
        }
    }
}