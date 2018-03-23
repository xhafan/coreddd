using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests
{
    [TestFixture]
    public class when_resolving_all_services_generic
    {
        private interface IServiceType { }
        private class ServiceTypeOne : IServiceType { }
        private class ServiceTypeTwo : IServiceType { }

        private IContainer _container;
        private IEnumerable<IServiceType> _result;
        private ServiceTypeOne _serviceTypeOne;
        private ServiceTypeTwo _serviceTypeTwo;


        [SetUp]
        public void Context()
        {
            _container = A.Fake<IContainer>();
            IoC.Initialize(_container);

            _serviceTypeOne = new ServiceTypeOne();
            _serviceTypeTwo = new ServiceTypeTwo();
            A.CallTo(() => _container.ResolveAll<IServiceType>()).Returns(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });

            _result = IoC.ResolveAll<IServiceType>();
        }

        [Test]
        public void all_service_types_is_resolved()
        {
            _result.ShouldBe(new IServiceType[] { _serviceTypeOne, _serviceTypeTwo });
        }
    }
}