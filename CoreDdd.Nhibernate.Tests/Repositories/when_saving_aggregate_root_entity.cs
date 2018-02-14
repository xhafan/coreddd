using FakeItEasy;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests.Repositories
{
    [TestFixture]
    public class when_saving_aggregate_root_entity : NhibernateRepositorySetup
    {
        [SetUp]
        public override void Context()
        {
            base.Context();
            
            Repository.Save(Entity);
        }

        [Test]
        public void get_by_id_was_called_on_session()
        {
            A.CallTo(() => Session.Save(Entity)).MustHaveHappened();
        }
    }
}