using System;
using Ninject;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Ninject.Tests
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
        private StandardKernel _kernel;

        [SetUp]
        public void Context()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IServiceType>().To<ServiceType>().InThreadScope();
            var ninjectContainer = new NinjectContainer(_kernel);

            _result = ninjectContainer.Resolve<IServiceType>();

            ninjectContainer.Release(_result);
        }

        [Test]
        public void service_is_released_from_container()
        {
            _result.IsDisposed.ShouldBeTrue();
        }
    }
}