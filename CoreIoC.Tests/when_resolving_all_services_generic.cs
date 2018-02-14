using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests
{
    [TestFixture]
    public class when_resolving_all_services_generic
    {
        private IContainer container;
        private interface IServiceType { }
        private class ServiceTypeOne : IServiceType { }
        private class ServiceTypeTwo : IServiceType { }

        private IServiceType[] _result;
        private ServiceTypeOne _serviceTypeOne;
        private ServiceTypeTwo _serviceTypeTwo;


        [SetUp]
        public void Context()
        {
            container = A.Fake<IContainer>();
            IoC.Initialize(container);

            _serviceTypeOne = new ServiceTypeOne();
            _serviceTypeTwo = new ServiceTypeTwo();
            A.CallTo(() => container.ResolveAll<IServiceType>()).Returns(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });

            _result = IoC.ResolveAll<IServiceType>();
        }

        [Test]
        public void all_service_types_is_resolved()
        {
            _result.ShouldBe(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });
        }
    }
}