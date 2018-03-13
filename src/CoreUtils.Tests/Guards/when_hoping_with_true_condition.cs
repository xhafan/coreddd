using System;
using NUnit.Framework;

namespace CoreUtils.Tests.Guards
{
    [TestFixture]
    public class when_hoping_with_true_condition
    {
        [Test]
        public void no_exception_is_thrown()
        {
            Guard.Hope<Exception>(true, "exception message");
        }
    }
}