using System;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace Core.Commons.Tests.UnitOfWorkTests
{
    [TestFixture]
    public class when_getting_session_factory
    {
        [Test]
        public void not_initialized_throws_an_exception()
        {
            var nhibernateConfigurator = MockRepository.GenerateStub<INHibernateConfigurator>();
            nhibernateConfigurator.Stub(x => x.GetSessionFactory()).Return(null);
            UnitOfWork.Initialize(nhibernateConfigurator);
            
            Should.Throw<InvalidOperationException>(() => { var sessionFactory = UnitOfWork.SessionFactory; });
        }

        [Test]
        public void initialized_returns_session_factory()
        {
            var sessionFactory = MockRepository.GenerateStub<ISessionFactory>();
            var nhibernateConfigurator = MockRepository.GenerateStub<INHibernateConfigurator>();
            nhibernateConfigurator.Stub(x => x.GetSessionFactory()).Return(sessionFactory);
            UnitOfWork.Initialize(nhibernateConfigurator);
            
            var retrievedSessionFactory = UnitOfWork.SessionFactory;

            retrievedSessionFactory.ShouldBe(sessionFactory);
        }    
    }
}