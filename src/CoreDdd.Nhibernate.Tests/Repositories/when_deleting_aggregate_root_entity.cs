using FakeItEasy;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_deleting_aggregate_root_entity : NhibernateRepositorySetup
    {    
        [SetUp]
        public override void Context()
        {
            base.Context();

            Repository.Delete(Entity);
        }

        [Test]
        public void get_by_id_was_called_on_session()
        {
            A.CallTo(() => Session.Delete(Entity)).MustHaveHappened();
        }
    }
}