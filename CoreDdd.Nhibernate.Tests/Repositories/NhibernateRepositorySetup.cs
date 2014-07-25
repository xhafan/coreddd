using CoreDdd.Domain;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreTest;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    public abstract class NhibernateRepositorySetup : BaseTest
    {
        protected ISession Session;
        protected TestEntity Entity;
        protected NhibernateRepository<TestEntity> Repository;

        protected class TestEntity : Entity, IAggregateRoot
        {
        }

        [SetUp]
        public virtual void Context()
        {
            Entity = new TestEntity();
            Session = Mock<ISession>();
            var unitOfWork = CreateUnitOfWorkWithStartedTransaction();
            Repository = new NhibernateRepository<TestEntity>(unitOfWork);
        }

        private NhibernateUnitOfWork CreateUnitOfWorkWithStartedTransaction()
        {
            var sessionFactory = Stub<ISessionFactory>().Stubs(x => x.OpenSession()).Returns(Session);
            var nhibernateConfigurator =
                Stub<INhibernateConfigurator>().Stubs(x => x.GetSessionFactory()).Returns(sessionFactory);
            var unitOfWork = new NhibernateUnitOfWork(nhibernateConfigurator);
            unitOfWork.BeginTransaction();
            return unitOfWork;
        }
    }
}