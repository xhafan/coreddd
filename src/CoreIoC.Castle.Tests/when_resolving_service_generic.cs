using Castle.Windsor;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Castle.Tests
{
    [TestFixture]
    public class when_resolving_service_generic
    {
        private interface IServiceType { }
        private class ServiceType : IServiceType { }

        private IWindsorContainer _windsorContainer;
        private CastleContainer _castleContainer;
        private IServiceType _result;
        private ServiceType _serviceType;

        [SetUp]
        public void Context()
        {
            _windsorContainer = A.Fake<IWindsorContainer>();
            _castleContainer = new CastleContainer(_windsorContainer);

            _serviceType = new ServiceType();
            A.CallTo(() => _windsorContainer.Resolve<IServiceType>()).Returns(_serviceType);

            _result = _castleContainer.Resolve<IServiceType>();
        }

        [Test]
        public void service_type_is_resolved()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}