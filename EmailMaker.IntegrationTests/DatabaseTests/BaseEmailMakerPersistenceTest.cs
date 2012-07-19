using Core.Tests.Helpers.Persistence;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public abstract class BaseEmailMakerPersistenceTest : BasePersistenceTest
    {
        protected override void ConfigureNHibernate()
        {
            Session = EmailMakerPersistenceTestHelper.ConfigureNHibernate();
        }

        protected override void ClearDatabase()
        {
            EmailMakerPersistenceTestHelper.ClearDatabase(Session);
        }
    }
}