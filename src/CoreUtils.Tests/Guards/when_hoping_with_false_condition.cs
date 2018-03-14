using System;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.Guards
{
    [TestFixture]
    public class when_hoping_with_false_condition
    {
        private const string ExceptionMessage = "exception message";
        private Exception _exception;

        [SetUp]
        public void Context()
        {
            _exception = Should.Throw<Exception>(() => Guard.Hope(false, ExceptionMessage));
        }

        [Test]
        public void exception_is_thrown()
        {
            _exception.Message.ShouldBe(ExceptionMessage);
        }
    }
}