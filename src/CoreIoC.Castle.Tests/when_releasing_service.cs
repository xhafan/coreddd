using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Castle.Tests
{
    [TestFixture]
    public class when_releasing_service
    {
        private interface IServiceType : IDisposable
        {
            bool IsDisposed { get; }
        }

        protected class ServiceType : IServiceType
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

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

            castleContainer.Release(_result);
        }

        [Test]
        public void service_is_released_from_container()
        {
            _result.IsDisposed.ShouldBeTrue();
        }

        [TearDown]
        public void TearDown()
        {
            _windsorContainer.Dispose();
        }
    }
}