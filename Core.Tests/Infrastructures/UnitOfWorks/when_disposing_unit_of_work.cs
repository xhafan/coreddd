using Core.Infrastructure;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Tests.Infrastructures.UnitOfWorks
{
    [TestFixture]
    public class when_disposing_unit_of_work
    {
        private ISession _session;

        [SetUp]
        public void Context()
        {
            _session = MockRepository.GenerateStub<ISession>();
            var unitOfWork = new UnitOfWork(_session);
            unitOfWork.Dispose();
        }

        [Test]
        public void dispose_was_called_on_session()
        {
            _session.AssertWasCalled(a => a.Dispose());
        }
    }
}