using System;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_committing_with_an_exception : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        private ITransaction _transaction;
        private Exception _exception;
        private Exception _thrownException;

        [SetUp]
        public override void Context()
        {
            base.Context();

            _transaction = Mock<ITransaction>();
            _exception = new Exception();
            _transaction.Stub(x => x.Commit()).Throw(_exception);
            Session.Stubs(x => x.Transaction).Returns(_transaction);

            _thrownException = Should.Throw<Exception>(() => UnitOfWork.Commit());
        }

        [Test]
        public void rollback_was_called_on_transaction()
        {
            _transaction.AssertWasCalled(x => x.Rollback());
        }

        [Test]
        public void transaction_was_disposed()
        {
            _transaction.AssertWasCalled(x => x.Dispose());
        }

        [Test]
        public void correct_exception_is_thrown()
        {
            _thrownException.ShouldBe(_exception);
        }

        [Test]
        public void session_was_disposed()
        {
            Session.AssertWasCalled(x => x.Dispose());
        }

        [Test]
        public void session_was_set_to_null()
        {
            UnitOfWork.Session.ShouldBe(null);
        }

        [Test]
        public void unit_of_work_is_not_active()
        {
            UnitOfWork.IsActive().ShouldBe(false);
        }
    }
}