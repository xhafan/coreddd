using Ninject;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Ninject.Tests
{
    [TestFixture]
    public class when_resolving_service
    {
        private interface IServiceType {}
        protected class ServiceType : IServiceType { }

        private object _result;

        [SetUp]
        public void Context()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IServiceType>().To<ServiceType>();
            var ninjectContainer = new NinjectContainer(kernel);

            _result = ninjectContainer.Resolve(typeof(IServiceType));
        }

        [Test]
        public void service_is_resolved()
        {
            _result.ShouldBeOfType<ServiceType>();
        }
    }
}