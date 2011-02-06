using System;
using NUnit.Framework;

namespace Core.Commons.Tests.IoCTests
{
    [TestFixture]
    public class when_getting_container_without_initialization
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "The container has not been initialized! Please call IoC.Initialize(container) before using it.")]
        public void Context()
        {
            var container = IoC.Container;
        }
    }
}