using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreTest;
using FakeItEasy;
using NHibernate;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    public abstract class NhibernateUnitOfWorkSetup : BaseTest
    {
        protected ISession Session;
        protected NhibernateUnitOfWork UnitOfWork;
        protected ITransaction Transaction;

        [SetUp]
        public virtual void Context()
        {
            Transaction = A.Fake<ITransaction>();
            Session = A.Fake<ISession>();
            A.CallTo(() => Session.Transaction).Returns(Transaction);

            var sessionFactory = A.Fake<ISessionFactory>();
            A.CallTo(() => sessionFactory.OpenSession()).Returns(Session);
            var nhibernateConfigurator = A.Fake<INhibernateConfigurator>();
            A.CallTo(() => nhibernateConfigurator.GetSessionFactory()).Returns(sessionFactory);
            
            UnitOfWork = new NhibernateUnitOfWork(nhibernateConfigurator);
        }
    }
}