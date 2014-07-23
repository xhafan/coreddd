using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreTest;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    public abstract class NhibernateUnitOfWorkSetup : BaseTest
    {
        protected ISession Session;
        protected NhibernateUnitOfWork UnitOfWork;

        [SetUp]
        public virtual void Context()
        {
            Session = Stub<ISession>();
            var sessionFactory = Stub<ISessionFactory>().Stubs(x => x.OpenSession()).Returns(Session);
            var nhibernateConfigurator = Stub<INhibernateConfigurator>().Stubs(x => x.GetSessionFactory()).Returns(sessionFactory);
            UnitOfWork = new NhibernateUnitOfWork(nhibernateConfigurator);
        }
    }
}