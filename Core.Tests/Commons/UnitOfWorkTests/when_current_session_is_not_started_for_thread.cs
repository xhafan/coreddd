using Core.Infrastructure;
using NUnit.Framework;
using Shouldly;

namespace Core.Tests.Commons.UnitOfWorkTests
{
    [TestFixture]
    public class when_current_session_is_not_started_for_thread
    {
        private bool _isStarted;

        [SetUp]
        public void Context()
        {
            _isStarted = UnitOfWork.IsStarted;
        }

        [Test]
        public void session_is_correct()
        {
            _isStarted.ShouldBe(false);
        }
    }
}