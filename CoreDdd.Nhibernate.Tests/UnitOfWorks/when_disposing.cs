using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_disposing : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            UnitOfWork.Dispose();
        }

        [Test]
        public void commit_was_called_on_transaction()
        {
            A.CallTo(() => Transaction.Commit()).MustHaveHappened();
        }

        [Test]
        public void transaction_was_disposed()
        {
            A.CallTo(() => Transaction.Dispose()).MustHaveHappened();
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