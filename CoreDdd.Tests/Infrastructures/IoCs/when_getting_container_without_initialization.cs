using System;
using CoreDdd.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Infrastructures.IoCs
{
    [TestFixture]
    public class when_getting_container_without_initialization
    {
        [Test]
        public void Context()
        {
            IoC.Initialize(null);

            var ex = Should.Throw<InvalidOperationException>(() => { var container = IoC.Container; });

            ex.Message.ToLower().ShouldMatch("container.*not.*initialized");
        }
    }
}