using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreIoC.Castle.Tests.CastleContainers
{
    [TestFixture]
    public class when_resolving_all_services_generic : CastleContainerSetup
    {
        private interface IServiceType { }
        private class ServiceTypeOne : IServiceType { }
        private class ServiceTypeTwo : IServiceType { }

        private IServiceType[] _result;
        private ServiceTypeOne _serviceTypeOne;
        private ServiceTypeTwo _serviceTypeTwo;


        [SetUp]
        public override void Context()
        {
            base.Context();
            _serviceTypeOne = new ServiceTypeOne();
            _serviceTypeTwo = new ServiceTypeTwo();
            WindsorContainer.Stub(x => x.ResolveAll<IServiceType>()).Return(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });

            _result = CastleContainer.ResolveAll<IServiceType>();
        }

        [Test]
        public void all_service_types_is_resolved()
        {
            _result.ShouldBe(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });
        }
    }
}