using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Castle.Tests
{
    [TestFixture]
    public class when_resolving_service
    {
        private interface IServiceType {}
        protected class ServiceType : IServiceType { }

        private object _result;
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

            _result = castleContainer.Resolve(typeof(IServiceType));
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