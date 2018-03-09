using CoreDdd.Domain;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreTest;
using FakeItEasy;
using NHibernate;
using NUnit.Framework;

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
            Session = A.Fake<ISession>();
            var unitOfWork = CreateUnitOfWorkWithStartedTransaction();
            Repository = new NhibernateRepository<TestEntity>(unitOfWork);
        }

        private NhibernateUnitOfWork CreateUnitOfWorkWithStartedTransaction()
        {
            var sessionFactory = A.Fake<ISessionFactory>();
            A.CallTo(() => sessionFactory.OpenSession()).Returns(Session);

            var nhibernateConfigurator = A.Fake<INhibernateConfigurator>();
            A.CallTo(() => nhibernateConfigurator.GetSessionFactory()).Returns(sessionFactory);

            var unitOfWork = new NhibernateUnitOfWork(nhibernateConfigurator);
            unitOfWork.BeginTransaction();

            return unitOfWork;
        }
    }
}