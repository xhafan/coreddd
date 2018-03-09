using CoreTest;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.Guards
{
    [TestFixture]
    public class when_hoping_with_false_condition : BaseTest
    {
        private const string ExceptionMessage = "exception message";
        private CoreException _coreException;

        [SetUp]
        public void Context()
        {
            _coreException = Should.Throw<CoreException>(() => Guard.Hope(false, ExceptionMessage));
        }

        [Test]
        public void exception_is_thrown()
        {
            _coreException.Message.ShouldBe(ExceptionMessage);
        }
    }
}