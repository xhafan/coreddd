using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Service.Tests.UnitOfWorkMessageModules
{
    [TestFixture]
    public class when_handling_begin_message : UnitOfWorkMessageModuleWithStartedTransactionSetup
    {
        [Test]
        public void unit_of_work_begins_transaction()
        {
            UnitOfWork.AssertWasCalled(x => x.BeginTransaction());
        }
    }
}