using FakeItEasy;
using NUnit.Framework;

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
            A.CallTo(() => UnitOfWork.Rollback()).MustHaveHappened();
        }
    }
}