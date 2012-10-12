using CoreTest;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public abstract class BaseEmailMakerPersistenceTest : BasePersistenceTest
    {
        protected override IAggregateRootTypesToClearProvider GetAggregateRootTypesToClearProvider()
        {
            return new EmailMakerAggregateRootTypesToClearProvider();
        }
    }
}