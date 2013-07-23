using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreIoC.Tests.IoCs
{
    [TestFixture]
    public class when_resolving_all_services_generic
    {
        private interface IServiceType { }
        private class ServiceTypeOne : IServiceType { }
        private class ServiceTypeTwo : IServiceType { }

        private IWindsorContainer _container;
        private IServiceType[] _result;
        private ServiceTypeOne _serviceTypeOne;
        private ServiceTypeTwo _serviceTypeTwo;


        [SetUp]
        public void Context()
        {
            _container = MockRepository.GenerateMock<IWindsorContainer>();
            _serviceTypeOne = new ServiceTypeOne();
            _serviceTypeTwo = new ServiceTypeTwo();
            _container.Stub(a => a.ResolveAll<IServiceType>()).Return(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });
            IoC.Initialize(_container);
            _result = IoC.ResolveAll<IServiceType>();
        }

        [Test]
        public void resolve_all_was_called_on_container()
        {
            _container.AssertWasCalled(a => a.ResolveAll<IServiceType>());
        }

        [Test]
        public void all_service_types_is_resolved()
        {
            _result.Length.ShouldBe(2);
            _result[0].ShouldBe(_serviceTypeOne);
            _result[1].ShouldBe(_serviceTypeTwo);
        }
    }
}