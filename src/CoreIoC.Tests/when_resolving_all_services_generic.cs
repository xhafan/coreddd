using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CoreIoC.Castle;
using CoreUtils.Extensions;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests
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
            _setupContainerToResolveTheServices();
            IoC.Initialize(new CastleContainer(_windsorContainer));


            _result = IoC.ResolveAll<IServiceType>();


            void _setupContainerToResolveTheServices()
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
            }
        }

        [Test]
        public void all_service_are_resolved()
        {
            _result.Count().ShouldBe(2);
            _result.First().ShouldBeOfType<ServiceTypeOne>();
            _result.Second().ShouldBeOfType<ServiceTypeTwo>();
        }
    }
}