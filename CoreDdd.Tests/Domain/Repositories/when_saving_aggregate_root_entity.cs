using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace CoreDdd.Tests.Domain.Repositories
{
    [TestFixture]
    public class when_saving_aggregate_root_entity
    {
        private ISession _session;
        private TestEntity _testEntity;

        private class TestEntity : Entity, IAggregateRoot
        {
        }

        [SetUp]
        public void Context()
        {
            _session = MockRepository.GenerateMock<ISession>();
            var repository = new NhibernateRepository<TestEntity>(_session);
            _testEntity = new TestEntity();
            repository.Save(_testEntity);
        }

        [Test]
        public void get_by_id_was_called_on_session()
        {
            _session.AssertWasCalled(a => a.Save(_testEntity));
        }
    }
}