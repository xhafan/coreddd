using Castle.Windsor;
using CoreDdd.Infrastructure;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreDdd.Tests.Infrastructures.IoCs
{
    [TestFixture]
    public class when_resolving_service_generic
    {
        private interface IServiceType { }
        private class ServiceType : IServiceType { }

        private IWindsorContainer _container;
        private ServiceType _result;
        private ServiceType _serviceType;


        [SetUp]
        public void Context()
        {
            _container = MockRepository.GenerateMock<IWindsorContainer>();
            _serviceType = new ServiceType();
            _container.Stub(a => a.Resolve<IServiceType>()).Return(_serviceType);
            IoC.Initialize(_container);
            _result = IoC.Resolve<IServiceType>() as ServiceType;
        }

        [Test]
        public void resolve_was_called_on_container()
        {
            _container.AssertWasCalled(a => a.Resolve<IServiceType>());
        }

        [Test]
        public void service_type_is_resolved()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}