using System.Data;
using Core.Infrastructure;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Tests.Commons.UnitOfWorkTests
{
    [TestFixture]
    public class when_beginning_transaction_with_isolation_level
    {
        private ISession _session;

        [SetUp]
        public void Context()
        {
            _session = MockRepository.GenerateStub<ISession>();
            var unitOfWork = new UnitOfWork(_session);
            unitOfWork.BeginTransaction(IsolationLevel.Serializable);
        }

        [Test]
        public void begin_transaction_was_called_on_session()
        {
            _session.AssertWasCalled(a => a.BeginTransaction(IsolationLevel.Serializable));
        }
    }
}