using FakeItEasy;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_committing_transaction_with_inactive_session : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();
            UnitOfWork.Rollback();

            UnitOfWork.Commit();
        }

        [Test]
        public void commit_was_not_called_on_transaction()
        {
            A.CallTo(() => Transaction.Commit()).MustNotHaveHappened();
        }
    }
}