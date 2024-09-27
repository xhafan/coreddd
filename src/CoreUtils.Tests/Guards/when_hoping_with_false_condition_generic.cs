using System;
using System.Collections.Generic;
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
            _exception = Should.Throw<InvalidOperationException>(() => 
                Guard.Hope<InvalidOperationException>(false, ExceptionMessage, new Dictionary<string, object>{{"key", "value"}}));
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