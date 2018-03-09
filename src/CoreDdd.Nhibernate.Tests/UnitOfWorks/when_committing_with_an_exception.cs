using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_committing_with_an_exception : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        private Exception _exception;
        private Exception _thrownException;

        [SetUp]
        public override void Context()
        {
            base.Context();

            _exception = new Exception();
            A.CallTo(() => Transaction.Commit()).Throws(_exception);

            _thrownException = Should.Throw<Exception>(() => UnitOfWork.Commit());
        }

        [Test]
        public void rollback_was_called_on_transaction()
        {
            A.CallTo(() => Transaction.Rollback()).MustHaveHappened();
        }

        [Test]
        public void transaction_was_disposed()
        {
            A.CallTo(() => Transaction.Dispose()).MustHaveHappened();
        }

        [Test]
        public void correct_exception_is_thrown()
        {
            _thrownException.ShouldBe(_exception);
        }

        [Test]
        public void session_was_disposed()
        {
            A.CallTo(() => Session.Dispose()).MustHaveHappened();
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