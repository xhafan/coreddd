using Castle.Windsor;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Castle.Tests
{
    [TestFixture]
    public class when_resolving_all_services_generic
    {
        private interface IServiceType { }
        private class ServiceTypeOne : IServiceType { }
        private class ServiceTypeTwo : IServiceType { }

        private IWindsorContainer _windsorContainer;
        private CastleContainer _castleContainer;
        private IServiceType[] _result;
        private ServiceTypeOne _serviceTypeOne;
        private ServiceTypeTwo _serviceTypeTwo;


        [SetUp]
        public void Context()
        {
            _windsorContainer = A.Fake<IWindsorContainer>();
            _castleContainer = new CastleContainer(_windsorContainer);

            _serviceTypeOne = new ServiceTypeOne();
            _serviceTypeTwo = new ServiceTypeTwo();
            A.CallTo(() => _windsorContainer.ResolveAll<IServiceType>()).Returns(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });

            _result = _castleContainer.ResolveAll<IServiceType>();
        }

        [Test]
        public void all_service_types_is_resolved()
        {
            _result.ShouldBe(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });
        }
    }
}