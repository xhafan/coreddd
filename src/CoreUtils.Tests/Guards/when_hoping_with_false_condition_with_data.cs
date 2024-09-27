using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.Guards
{
    [TestFixture]
    public class when_hoping_with_false_condition_with_data
    {
        private const string ExceptionMessage = "exception message";
        private Exception _exception;

        [SetUp]
        public void Context()
        {
            _exception = Should.Throw<Exception>(() => Guard.Hope(false, ExceptionMessage, new Dictionary<string, object>{{"key", "value"}}));
        }

        [Test]
        public void exception_is_thrown()
        {
            _exception.Message.ShouldBe(ExceptionMessage);
        }
        
        [Test]
        public void exception_data_is_set()
        {
            _exception.Data.Keys.ShouldBe(new[] {"key"});
            _exception.Data.Values.ShouldBe(new[] {"value"});
        }
    }
}