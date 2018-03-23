using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Castle.Tests
{
    [TestFixture]
    public class when_resolving_service_generic
    {
        private interface IServiceType { }
        protected class ServiceType : IServiceType { }

        private IWindsorContainer _windsorContainer;
        private CastleContainer _castleContainer;
        private IServiceType _result;

        [SetUp]
        public void Context()
        {
            _windsorContainer = new WindsorContainer();

            _windsorContainer.Register(
                Component.For<IServiceType>()
                    .ImplementedBy<ServiceType>()
                    .LifeStyle.Transient
            );

            _castleContainer = new CastleContainer(_windsorContainer);

            _result = _castleContainer.Resolve<IServiceType>();
        }

        [Test]
        public void service_is_resolved()
        {
            _result.ShouldBeOfType<ServiceType>();
        }
    }
}