using System.Collections.Generic;
using System.Linq;
using CoreUtils.Extensions;
using Ninject;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Ninject.Tests
{
    [TestFixture]
    public class when_resolving_all_services_generic
    {
        private interface IServiceType { }
        protected class ServiceTypeOne : IServiceType { }
        protected class ServiceTypeTwo : IServiceType { }

        private IEnumerable<IServiceType> _result;


        [SetUp]
        public void Context()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IServiceType>().To<ServiceTypeOne>();
            kernel.Bind<IServiceType>().To<ServiceTypeTwo>();
            var ninjectContainer = new NinjectContainer(kernel);

            _result = ninjectContainer.ResolveAll<IServiceType>();
        }

        [Test]
        public void all_service_are_resolved()
        {
            _result.Count().ShouldBe(2);
            _result.First().ShouldBeOfType<ServiceTypeOne>();
            _result.Second().ShouldBeOfType<ServiceTypeTwo>();
        }
    }
}