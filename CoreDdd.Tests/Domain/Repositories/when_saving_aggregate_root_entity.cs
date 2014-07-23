using NUnit.Framework;
using Rhino.Mocks;

namespace CoreDdd.Tests.Domain.Repositories
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
            Session.AssertWasCalled(x => x.Save(Entity));
        }
    }
}