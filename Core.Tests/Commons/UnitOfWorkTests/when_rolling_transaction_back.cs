using Core.Infrastructure;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Tests.Commons.UnitOfWorkTests
{
    [TestFixture]
    public class when_rolling_transaction_back
    {
        private ISession _session;
        private ITransaction _transaction;

        [SetUp]
        public void Context()
        {
            _session = MockRepository.GenerateStub<ISession>();
            _transaction = MockRepository.GenerateMock<ITransaction>();
            _session.Stub(a => a.Transaction).Return(_transaction);
            
            var unitOfWork = new UnitOfWork(_session);
            unitOfWork.TransactionalRollback();
        }

        [Test]
        public void commit_was_called_on_transaction()
        {
            _transaction.AssertWasCalled(a => a.Rollback());
        }
    }
}