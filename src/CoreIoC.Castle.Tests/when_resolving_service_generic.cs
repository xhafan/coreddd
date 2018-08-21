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
        private WindsorContainer _windsorContainer;

        [SetUp]
        public void Context()
        {
            _windsorContainer = new WindsorContainer();

            _windsorContainer.Register(
                Component.For<IServiceType>()
                    .ImplementedBy<ServiceType>()
                    .LifeStyle.Transient
            );

            var castleContainer = new CastleContainer(_windsorContainer);

            _result = castleContainer.Resolve<IServiceType>();
        }

        [Test]
        public void service_is_resolved()
        {
            _result.ShouldBeOfType<ServiceType>();
        }

        [TearDown]
        public void TearDown()
        {
            _windsorContainer.Dispose();
        }
    }
}