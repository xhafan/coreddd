using CoreTest;
using NUnit.Framework;

namespace CoreIoC.Tests.IoCs
{
    public abstract class IoCSetup : BaseTest
    {
        protected IContainer Container;

        [SetUp]
        public virtual void Context()
        {
            Container = Stub<IContainer>();

            IoC.Initialize(Container);
        }
    }
}
