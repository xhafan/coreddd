using Ninject;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Ninject.Tests
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
            var kernel = new StandardKernel();
            kernel.Bind<IServiceType>().To<ServiceType>();
            var ninjectContainer = new NinjectContainer(kernel);

            _result = ninjectContainer.Resolve<IServiceType>();
        }

        [Test]
        public void service_is_resolved()
        {
            _result.ShouldBeOfType<ServiceType>();
        }
    }
}