using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CoreIoC.Castle;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests
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
            _setupContainerToResolveTheService();
            IoC.Initialize(new CastleContainer(_windsorContainer));
            _result = IoC.Resolve<IServiceType>();


            IoC.Release(_result);


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
        public void service_is_released_from_container()
        {
            _result.IsDisposed.ShouldBeTrue();
        }
    }
}