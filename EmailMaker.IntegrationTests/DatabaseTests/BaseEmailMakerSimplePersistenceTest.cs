using Core.Tests.Helpers.Persistence;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public abstract class BaseEmailMakerSimplePersistenceTest : BaseSimplePersistenceTest
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