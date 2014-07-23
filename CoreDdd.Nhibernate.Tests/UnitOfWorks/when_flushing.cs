using NUnit.Framework;
using Rhino.Mocks;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_flushing : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            UnitOfWork.Flush();
        }

        [Test]
        public void flush_was_called_on_session()
        {
            Session.AssertWasCalled(x => x.Flush());
        }
    }
}