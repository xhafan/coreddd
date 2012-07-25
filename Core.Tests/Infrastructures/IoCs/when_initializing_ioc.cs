using Castle.Windsor;
using Core.Infrastructure;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace Core.Tests.Infrastructures.IoCs
{
    [TestFixture]
    public class when_initializing_ioc
    {
        private IWindsorContainer _container;

        [SetUp]
        public void Context()
        {
            _container = MockRepository.GenerateStub<IWindsorContainer>();
            IoC.Initialize(_container);
        }

        [Test]
        public void container_is_correctly_set()
        {
            IoC.Container.ShouldBe(_container);
        }
    }
}
