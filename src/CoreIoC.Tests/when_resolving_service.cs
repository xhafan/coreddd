using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests
{
    [TestFixture]
    public class when_resolving_service
    {
        private interface IServiceType {}
        private class ServiceType : IServiceType { }

        private IContainer _container;
        private object _result;
        private ServiceType _serviceType;
        private Type _iServiceType;

        [SetUp]
        public void Context()
        {
            _container = A.Fake<IContainer>();
            IoC.Initialize(_container);

            _serviceType = new ServiceType();
            _iServiceType = typeof(IServiceType);
            A.CallTo(() => _container.Resolve(_iServiceType)).Returns(_serviceType);

            _result = IoC.Resolve(_iServiceType);
        }

        [Test]
        public void container_is_correctly_set()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}