using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace Core.Commons.Tests.IoCTests
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
            _container.Expect(a => a.Resolve<IServiceType>()).Return(_serviceType);
            IoC.Initialize(_container);
            _result = IoC.Resolve<IServiceType>() as ServiceType;
        }

        [Test]
        public void resolve_was_called_on_container()
        {
            _container.AssertWasCalled(a => a.Resolve<IServiceType>());
        }

        [Test]
        public void container_is_correctly_set()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}