using NUnit.Framework;
using Rhino.Mocks;

namespace CoreDdd.Tests.Domain.Repositories
{
    [TestFixture]
    public class when_getting_aggregate_root_entity_by_id : NhibernateRepositorySetup
    {
        private const int Id = 1;

        [SetUp]
        public override void Context()
        {
            base.Context();

            Repository.GetById(Id);
        }

        [Test]
        public void get_by_id_was_called_on_session()
        {
            Session.AssertWasCalled(x => x.Get<TestEntity>(Id));
        }
    }
}
