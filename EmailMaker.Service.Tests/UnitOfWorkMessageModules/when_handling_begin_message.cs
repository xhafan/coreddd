using CoreDdd.Infrastructure;
using CoreTest;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Service.Tests.UnitOfWorkMessageModules
{
    [TestFixture]
    public class when_handling_begin_message : UnitOfWorkMessageModuleWithStartedTransactionSetup
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Context()
        {
            _unitOfWork = Mock<IUnitOfWork>();
            var module = new UnitOfWorkMessageModule(_unitOfWork);

            module.HandleBeginMessage();
        }

        [Test]
        public void unit_of_work_begins_transaction()
        {
            _unitOfWork.AssertWasCalled(x => x.BeginTransaction());
        }
    }
}