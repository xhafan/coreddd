using CoreIoC.Ninject;
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
#if NET40
            var kernel = new StandardKernel();
#else
            var kernel = new KernelConfiguration();
#endif
            kernel.Bind<IServiceType>().To<ServiceType>();

#if NET40
            var ninjectContainer = new NinjectContainer(kernel);
#else
            var ninjectContainer = new NinjectContainer(kernel.BuildReadonlyKernel());
#endif

            _result = ninjectContainer.Resolve<IServiceType>();
        }

        [Test]
        public void service_is_resolved()
        {
            _result.ShouldBeOfType<ServiceType>();
        }
    }
}