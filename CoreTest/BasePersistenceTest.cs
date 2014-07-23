using System.Linq;
using CoreDdd.Domain;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using CoreUtils;
using CoreUtils.Extensions;
using NHibernate;
using NUnit.Framework;

namespace CoreTest
{
    public abstract class BasePersistenceTest
    {
        protected ISession Session;
        private NhibernateUnitOfWork _unitOfWork;

        protected abstract void Context();

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction();
            Session = _unitOfWork.Session;

            ClearDatabase();
            Context();
        }
        
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _unitOfWork.Commit();
        }

        protected abstract IAggregateRootTypesToClearProvider GetAggregateRootTypesToClearProvider();

        protected void ClearDatabase()
        {
            GetAggregateRootTypesToClearProvider().GetAggregateRootTypesToClear().Each(x =>
                {
                    var typeName = x.Name;
                    var isAggregateRoot = x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IAggregateRoot<>));
                    Guard.Hope(isAggregateRoot, typeName + " is not implementing " + typeof(IAggregateRoot).Name);
                    Session.Delete("from " + typeName);
                });
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