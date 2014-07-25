using System;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreIoC.Castle.Tests.CastleContainers
{
    [TestFixture]
    public class when_resolving_service : CastleContainerSetup
    {
        private interface IServiceType {}
        private class ServiceType : IServiceType { }
        
        private object _result;
        private ServiceType _serviceType;
        private Type _iServiceType;

        [SetUp]
        public override void Context()
        {
            base.Context();
            _serviceType = new ServiceType();
            _iServiceType = typeof(IServiceType);
            WindsorContainer.Stub(x => x.Resolve(_iServiceType)).Return(_serviceType);

            _result = CastleContainer.Resolve(_iServiceType);
        }

        [Test]
        public void container_is_correctly_set()
        {
            _result.ShouldBe(_serviceType);
        }
    }
}