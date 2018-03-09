using FakeItEasy;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_loading_aggregate_root_entity_by_id : NhibernateRepositorySetup
    {
        private const int Id = 1;

        [SetUp]
        public override void Context()
        {
            base.Context();

            Repository.Load(Id);
        }

        [Test]
        public void get_by_id_was_called_on_session()
        {
            A.CallTo(() => Session.Load<TestEntity>(Id)).MustHaveHappened();
        }
    }
}