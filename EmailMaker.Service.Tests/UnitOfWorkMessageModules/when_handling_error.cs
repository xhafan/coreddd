using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Service.Tests.UnitOfWorkMessageModules
{
    [TestFixture]
    public class when_handling_error : UnitOfWorkMessageModuleWithStartedTransactionSetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            Module.HandleError();
        }

        [Test]
        public void unit_of_work_rolls_back()
        {
            UnitOfWork.AssertWasCalled(x => x.Rollback());
        }
    }
}