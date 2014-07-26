using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using CoreTest;
using NHibernate;

namespace CoreIntegrationTest.Nhibernate
{
    public abstract class BaseNhibernatePersistenceTest : BasePersistenceTest
    {
        protected ISession Session;

        protected override IUnitOfWork ResolveUnitOfWork()
        {
            return IoC.Resolve<NhibernateUnitOfWork>();
        }

        private NhibernateUnitOfWork NhibernateUnitOfWork
        {
            get { return (NhibernateUnitOfWork) UnitOfWork; }
        }

        protected override void GetSessionFromUnitOfWork()
        {
            Session = NhibernateUnitOfWork.Session;
        }

        protected override void SaveOrUpdate(object entity)
        {
            Session.SaveOrUpdate(entity);
        }

        protected override void Flush()
        {
            Session.Flush();
        }

        protected override TAggregateRoot Get<TAggregateRoot>(int id)
        {
            return Session.Get<TAggregateRoot>(id);
        }

        protected override void Delete(string query)
        {
            Session.Delete(query);
        }
    }
}