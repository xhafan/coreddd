using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CoreIoC.Castle;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests
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
            _setupContainerToResolveTheService();
            IoC.Initialize(new CastleContainer(_windsorContainer));


            _result = IoC.Resolve(typeof(IServiceType));


            void _setupContainerToResolveTheService()
            {
                _windsorContainer = new WindsorContainer();

                _windsorContainer.Register(
                    Component.For<IServiceType>()
                        .ImplementedBy<ServiceType>()
                        .LifeStyle.Transient
                );
            }
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