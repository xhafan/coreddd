using CoreIntegrationTest.Nhibernate;
using CoreTest;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public abstract class BaseEmailMakerPersistenceTest : BaseNhibernatePersistenceTest
    {
        protected override IAggregateRootTypesToClearProvider GetAggregateRootTypesToClearProvider()
        {
            return new EmailMakerAggregateRootTypesToClearProvider();
        }
    }
}