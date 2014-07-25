using System;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.Tests.IoCs
{
    [TestFixture]
    public class when_getting_container_without_initialization
    {
        [Test]
        public void exception_is_thrown()
        {
            IoC.Initialize(null);

            var ex = Should.Throw<InvalidOperationException>(() => IoC.Resolve<object>());

            ex.Message.ToLower().ShouldMatch("container.*not.*initialized");
        }
    }
}