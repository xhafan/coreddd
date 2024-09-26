using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests
{
    [TestFixture]
    public class when_uninitializing_ioc
    {
        [Test]
        public void ioc_is_not_initialized()
        {
            IoC.Initialize(new FakeContainer());
            
            IoC.Uninitialize();
            
            IoC.IsInitialized().ShouldBe(false);
        }
        
        private class FakeContainer : IContainer
        {
            public object Resolve(Type serviceType)
            {
                throw new NotImplementedException();
            }

            public TService Resolve<TService>()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<TService> ResolveAll<TService>()
            {
                throw new NotImplementedException();
            }

            public void Release(object service)
            {
                throw new NotImplementedException();
            }
        }
    }
}