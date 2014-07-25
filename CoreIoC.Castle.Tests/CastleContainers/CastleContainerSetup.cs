using Castle.Windsor;
using CoreTest;
using NUnit.Framework;

namespace CoreIoC.Castle.Tests.CastleContainers
{
    public abstract class CastleContainerSetup : BaseTest
    {
        protected IWindsorContainer WindsorContainer;
        protected CastleContainer CastleContainer;

        [SetUp]
        public virtual void Context()
        {
            WindsorContainer = Stub<IWindsorContainer>();

            CastleContainer = new CastleContainer(WindsorContainer);
        }
    }
}
