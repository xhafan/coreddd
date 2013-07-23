using System;
using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreIoC.Tests.IoCs
{
    [TestFixture]
    public class when_resolving_service
    {
        private interface IServiceType {}
        private class ServiceType : IServiceType { }
        
        private IWindsorContainer _container;
        private ServiceType _result;
        private ServiceType _serviceType;
        private Type _iServiceType;


        [SetUp]
        public void Context()
        {
            _container = MockRepository.GenerateMock<IWindsorContainer>();
            _serviceType = new ServiceType();
            _iServiceType = typeof (IServiceType);
            _container.Expect(a => a.Resolve(_iServiceType)).Return(_serviceType);
            IoC.Initialize(_container);
            _result = IoC.Resolve(_iServiceType) as ServiceType;
        }

        [Test]
        public void resolve_was_called_on_container()
        {
            _container.AssertWasCalled(a => a.Resolve(_iServiceType));
        }

        [Test]
        public void container_is_correctly_set()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}