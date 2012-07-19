using Core.Domain;
using Core.Domain.Persistence;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Tests.Domain.Persistence.Repositories
{
    [TestFixture]
    public class when_getting_aggregate_root_entity_by_id
    {
        private ISession _session;

        private abstract class TestEntity : Identity<TestEntity>, IAggregateRootEntity
        {
        }
        
        [SetUp]
        public void Context()
        {
            _session = MockRepository.GenerateMock<ISession>();
            var repository = new NhibernateRepository<TestEntity>(_session);
            repository.GetById(1);
        }

        [Test]
        public void get_by_id_was_called_on_session()
        {
            _session.AssertWasCalled(a => a.Get<TestEntity>(1));
        }
    }
}
