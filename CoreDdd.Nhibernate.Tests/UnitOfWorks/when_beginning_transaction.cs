using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_beginning_transaction : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        [Test]
        public void session_is_opened()
        {
            UnitOfWork.Session.ShouldBe(Session);
        }

        [Test]
        public void begin_transaction_was_called_on_session()
        {
            A.CallTo(() => Session.BeginTransaction()).MustHaveHappened();
        }

        [Test]
        public void unit_of_work_is_active()
        {
            UnitOfWork.IsActive().ShouldBe(true);
        }
    }
}