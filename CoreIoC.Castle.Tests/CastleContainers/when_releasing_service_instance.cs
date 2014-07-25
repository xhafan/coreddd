using NUnit.Framework;
using Rhino.Mocks;

namespace CoreIoC.Castle.Tests.CastleContainers
{
    [TestFixture]
    public class when_releasing_service_instance : CastleContainerSetup
    {
        private object _serviceInstance;

        [SetUp]
        public override void Context()
        {
            base.Context();
            _serviceInstance = new object();

            CastleContainer.Release(_serviceInstance);
        }

        [Test]
        public void release_was_called_on_container()
        {
            WindsorContainer.AssertWasCalled(x => x.Release(_serviceInstance));
        }
    }
}