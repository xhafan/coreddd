using FakeItEasy;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_clearing : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            UnitOfWork.Clear();
        }

        [Test]
        public void flush_was_called_on_session()
        {
            A.CallTo(() => Session.Clear()).MustHaveHappened();
        }
    }
}