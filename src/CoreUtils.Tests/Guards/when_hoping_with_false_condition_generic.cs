using System;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.Guards
{
    [TestFixture]
    public class when_hoping_with_false_condition_generic
    {
        private const string ExceptionMessage = "exception message";
        private InvalidOperationException _exception;

        [SetUp]
        public void Context()
        {
            _exception = Should.Throw<InvalidOperationException>(() => Guard.Hope<InvalidOperationException>(false, ExceptionMessage));
        }

        [Test]
        public void exception_is_thrown()
        {
            _exception.Message.ShouldBe(ExceptionMessage);
        }
    }
}