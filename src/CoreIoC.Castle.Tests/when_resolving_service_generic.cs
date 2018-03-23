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

        private IServiceType _result;

        [SetUp]
        public void Context()
        {
            var windsorContainer = new WindsorContainer();

            windsorContainer.Register(
                Component.For<IServiceType>()
                    .ImplementedBy<ServiceType>()
                    .LifeStyle.Transient
            );

            var castleContainer = new CastleContainer(windsorContainer);

            _result = castleContainer.Resolve<IServiceType>();
        }

        [Test]
        public void service_is_resolved()
        {
            _result.ShouldBeOfType<ServiceType>();
        }
    }
}