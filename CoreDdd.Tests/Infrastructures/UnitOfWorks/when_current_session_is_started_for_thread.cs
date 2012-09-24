using CoreDdd.Infrastructure;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreDdd.Tests.Infrastructures.UnitOfWorks
{
    [TestFixture]
    public class when_current_session_is_started_for_thread
    {
        private bool _isStarted;

        [SetUp]
        public void Context()
        {
            UnitOfWork.Current = new UnitOfWork(MockRepository.GenerateStub<ISession>());
            _isStarted = UnitOfWork.IsStarted;
        }

        [Test]
        public void session_is_correct()
        {
            _isStarted.ShouldBe(true);
        }
    }
}