using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace Core.Commons.Tests.UnitOfWorkTests
{
    [TestFixture]
    public class when_current_session_is_not_started_for_thread_after_dispose
    {
        private bool _isStarted;

        [SetUp]
        public void Context()
        {
            UnitOfWork.Current = new UnitOfWork(MockRepository.GenerateStub<ISession>());
            UnitOfWork.Current.Dispose();
            _isStarted = UnitOfWork.IsStarted;
        }

        [Test]
        public void session_is_correct()
        {
            _isStarted.ShouldBe(false);
        }
    }
}