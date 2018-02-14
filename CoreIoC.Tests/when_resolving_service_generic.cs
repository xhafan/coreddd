using FakeItEasy;
using NUnit.Framework;
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
            container = A.Fake<IContainer>();
            IoC.Initialize(container);

            _serviceType = new ServiceType();
            A.CallTo(() => container.Resolve<IServiceType>()).Returns(_serviceType);

            _result = IoC.Resolve<IServiceType>();
        }

        [Test]
        public void service_type_is_resolved()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}