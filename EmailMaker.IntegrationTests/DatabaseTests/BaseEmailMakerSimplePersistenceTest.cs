using CoreIntegrationTest.Nhibernate;
using CoreTest;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    public abstract class BaseEmailMakerSimplePersistenceTest : BaseNhibernateSimplePersistenceTest
    {
        protected override IAggregateRootTypesToClearProvider GetAggregateRootTypesToClearProvider()
        {
            return new EmailMakerAggregateRootTypesToClearProvider();
        }
    }
}