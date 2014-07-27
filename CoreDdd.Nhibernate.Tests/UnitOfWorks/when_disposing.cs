using NUnit.Framework;
using Rhino.Mocks;
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
            Transaction.AssertWasCalled(x => x.Commit());
        }

        [Test]
        public void transaction_was_disposed()
        {
            Transaction.AssertWasCalled(x => x.Dispose());
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