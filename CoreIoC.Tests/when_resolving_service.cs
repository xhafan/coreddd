using System;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreIoC.Tests
{
    [TestFixture]
    public class when_resolving_service
    {
        private IContainer container;
        private interface IServiceType {}
        private class ServiceType : IServiceType { }
        
        private object _result;
        private ServiceType _serviceType;
        private Type _iServiceType;

        [SetUp]
        public void Context()
        {
            container = MockRepository.GenerateStub<IContainer>();
            IoC.Initialize(container);

            _serviceType = new ServiceType();
            _iServiceType = typeof(IServiceType);
            container.Stub(x => x.Resolve(_iServiceType)).Return(_serviceType);

            _result = IoC.Resolve(_iServiceType);
        }

        [Test]
        public void container_is_correctly_set()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}