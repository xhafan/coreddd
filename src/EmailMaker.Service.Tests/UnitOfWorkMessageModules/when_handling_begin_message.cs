using FakeItEasy;
using NUnit.Framework;

namespace EmailMaker.Service.Tests.UnitOfWorkMessageModules
{
    [TestFixture]
    public class when_handling_begin_message : UnitOfWorkMessageModuleWithStartedTransactionSetup
    {
        [Test]
        public void unit_of_work_begins_transaction()
        {
            A.CallTo(() => UnitOfWork.BeginTransaction()).MustHaveHappened();
        }
    }
}