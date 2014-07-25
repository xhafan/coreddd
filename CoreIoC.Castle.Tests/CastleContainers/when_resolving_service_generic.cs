using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreIoC.Castle.Tests.CastleContainers
{
    [TestFixture]
    public class when_resolving_service_generic : CastleContainerSetup
    {
        private interface IServiceType { }
        private class ServiceType : IServiceType { }

        private IServiceType _result;
        private ServiceType _serviceType;

        [SetUp]
        public override void Context()
        {
            base.Context();
            _serviceType = new ServiceType();
            WindsorContainer.Stub(x => x.Resolve<IServiceType>()).Return(_serviceType);

            _result = CastleContainer.Resolve<IServiceType>();
        }

        [Test]
        public void service_type_is_resolved()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}