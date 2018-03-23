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

        private IWindsorContainer _windsorContainer;
        private CastleContainer _castleContainer;
        private IServiceType[] _result;


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

            _castleContainer = new CastleContainer(_windsorContainer);

            _result = _castleContainer.ResolveAll<IServiceType>();
        }

        [Test]
        public void all_service_are_resolved()
        {
            _result.Length.ShouldBe(2);
            _result.First().ShouldBeOfType<ServiceTypeOne>();
            _result.Second().ShouldBeOfType<ServiceTypeTwo>();
        }
    }
}