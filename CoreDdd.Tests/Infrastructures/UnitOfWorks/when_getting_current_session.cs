using CoreDdd.Infrastructure;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreDdd.Tests.Infrastructures.UnitOfWorks
{
    [TestFixture]
    public class when_getting_current_session
    {
        private ISession _session;

        [SetUp]
        public void Context()
        {
            _session = MockRepository.GenerateStub<ISession>();
            UnitOfWork.Current = new UnitOfWork(_session);
        }

        [Test]
        public void session_is_correct()
        {
            UnitOfWork.CurrentSession.ShouldBe(_session);
        }
    }
}