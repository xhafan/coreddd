using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests
{
    [TestFixture]
    public class when_checking_ioc_is_initialized
    {
        [Test]
        public void not_initialized()
        {
            IoC.Initialize(null!);

            IoC.IsInitialized().ShouldBe(false);
        }
        
        [Test]
        public void is_initialized()
        {
            IoC.Initialize(new FakeContainer());

            IoC.IsInitialized().ShouldBe(true);
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