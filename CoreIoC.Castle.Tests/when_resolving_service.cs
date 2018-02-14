using System;
using Castle.Windsor;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Castle.Tests
{
    [TestFixture]
    public class when_resolving_service
    {
        private interface IServiceType {}
        private class ServiceType : IServiceType { }

        private IWindsorContainer _windsorContainer;
        private CastleContainer _castleContainer;
        private object _result;
        private ServiceType _serviceType;
        private Type _iServiceType;

        [SetUp]
        public void Context()
        {
            _windsorContainer = A.Fake<IWindsorContainer>();
            _castleContainer = new CastleContainer(_windsorContainer);

            _serviceType = new ServiceType();
            _iServiceType = typeof(IServiceType);
            A.CallTo(() => _windsorContainer.Resolve(_iServiceType)).Returns(_serviceType);

            _result = _castleContainer.Resolve(_iServiceType);
        }

        [Test]
        public void container_is_correctly_set()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}