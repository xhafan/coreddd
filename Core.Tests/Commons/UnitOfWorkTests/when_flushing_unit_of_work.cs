using Core.Infrastructure;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Tests.Commons.UnitOfWorkTests
{
    [TestFixture]
    public class when_flushing_unit_of_work
    {
        private ISession _session;

        [SetUp]
        public void Context()
        {
            _session = MockRepository.GenerateStub<ISession>();
            var unitOfWork = new UnitOfWork(_session);
            unitOfWork.Flush();
        }

        [Test]
        public void flush_was_called_on_session()
        {
            _session.AssertWasCalled(a => a.Flush());
        }
    }
}