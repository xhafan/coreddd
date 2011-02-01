using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Commons.Tests
{
    public abstract class base_when_getting_current_unit_of_work
    {
        protected abstract void Context();

        [SetUp]
        public void Setup()
        {
            var sessionFactory = MockRepository.GenerateStub<ISessionFactory>();
            var session = MockRepository.GenerateStub<ISession>();
            sessionFactory.Stub(a => a.OpenSession()).Return(session);

            UnitOfWork.SessionFactory = sessionFactory;
            
            Context();
        }
    }
}