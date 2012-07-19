using Castle.Windsor;
using Core.Infrastructure;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Tests.Commons.IoCTests
{
    [TestFixture]
    public class when_releasing_service_instance
    {
        private IWindsorContainer _container;
        private object _serviceInstance;

        [SetUp]
        public void Context()
        {
            _container = MockRepository.GenerateStub<IWindsorContainer>();
            _serviceInstance = new object();
            _container.Expect(a => a.Release(_serviceInstance));
            IoC.Initialize(_container);
            IoC.Release(_serviceInstance);
        }

        [Test]
        public void release_called_on_container()
        {
            _container.AssertWasCalled(a => a.Release(_serviceInstance));
        }
    }
}