using CoreDdd.UnitOfWorks;
using CoreTest;
using FakeItEasy;
using NUnit.Framework;

namespace EmailMaker.Service.Tests.UnitOfWorkMessageModules
{
    public abstract class UnitOfWorkMessageModuleWithStartedTransactionSetup : BaseTest
    {
        protected IUnitOfWork UnitOfWork;
        protected UnitOfWorkMessageModule Module;

        [SetUp]
        public virtual void Context()
        {
            UnitOfWork = A.Fake<IUnitOfWork>();
            Module = new UnitOfWorkMessageModule(UnitOfWork);

            Module.HandleBeginMessage();
        }
    }
}