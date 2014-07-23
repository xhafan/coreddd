using NHibernate;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_getting_session : NhibernateUnitOfWorkWithStartedTransactionSetup
    {
        private ISession _retrievedSession;

        [SetUp]
        public override void Context()
        {
            base.Context();

            _retrievedSession = UnitOfWork.Session;
        }

        [Test]
        public void session_is_correct()
        {
            _retrievedSession.ShouldBe(Session);
        }
    }
}