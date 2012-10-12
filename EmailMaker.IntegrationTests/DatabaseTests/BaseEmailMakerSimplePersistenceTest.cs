using CoreTest;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public abstract class BaseEmailMakerSimplePersistenceTest : BaseSimplePersistenceTest
    {
        protected override IAggregateRootTypesToClearProvider GetAggregateRootTypesToClearProvider()
        {
            return new EmailMakerAggregateRootTypesToClearProvider();
        }
    }
}