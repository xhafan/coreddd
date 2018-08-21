using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CoreUtils.Extensions;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Castle.Tests
{
    [TestFixture]
    public class when_resolving_all_services_generic
    {
        private interface IServiceType { }
        protected class ServiceTypeOne : IServiceType { }
        protected class ServiceTypeTwo : IServiceType { }

        private IEnumerable<IServiceType> _result;
        private WindsorContainer _windsorContainer;

        [SetUp]
        public void Context()
        {
            _windsorContainer = new WindsorContainer();

            _windsorContainer.Register(
                Component.For<IServiceType>()
                    .ImplementedBy<ServiceTypeOne>()
                    .LifeStyle.Transient,
                Component.For<IServiceType>()
                    .ImplementedBy<ServiceTypeTwo>()
                    .LifeStyle.Transient
            );

            var castleContainer = new CastleContainer(_windsorContainer);

            _result = castleContainer.ResolveAll<IServiceType>();
        }

        [Test]
        public void all_service_are_resolved()
        {
            _result.Count().ShouldBe(2);
            _result.First().ShouldBeOfType<ServiceTypeOne>();
            _result.Second().ShouldBeOfType<ServiceTypeTwo>();
        }

        [TearDown]
        public void TearDown()
        {
            _windsorContainer.Dispose();
        }
    }
}