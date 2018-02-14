using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreIoC.Tests
{
    [TestFixture]
    public class when_resolving_service_generic
    {
        private IContainer container;
        private interface IServiceType { }
        private class ServiceType : IServiceType { }

        private IServiceType _result;
        private ServiceType _serviceType;

        [SetUp]
        public void Context()
        {
            container = MockRepository.GenerateStub<IContainer>();
            IoC.Initialize(container);

            _serviceType = new ServiceType();
            container.Stub(x => x.Resolve<IServiceType>()).Return(_serviceType);

            _result = IoC.Resolve<IServiceType>();
        }

        [Test]
        public void service_type_is_resolved()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}