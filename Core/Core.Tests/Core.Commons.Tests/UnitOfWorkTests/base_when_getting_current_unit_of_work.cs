using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Commons.Tests.UnitOfWorkTests
{
    public abstract class base_when_getting_current_unit_of_work
    {
        protected ISession Session;
        protected abstract void Context();

        [SetUp]
        public void SetUp()
        {
            var sessionFactory = MockRepository.GenerateStub<ISessionFactory>();
            Session = MockRepository.GenerateStub<ISession>();
            sessionFactory.Stub(a => a.OpenSession()).Return(Session);

            var nhibernateConfigurator = MockRepository.GenerateStub<INHibernateConfigurator>();
            nhibernateConfigurator.Stub(x => x.GetSessionFactory()).Return(sessionFactory);
            UnitOfWork.Initialize(nhibernateConfigurator);
            
            Context();
        }
    }
}