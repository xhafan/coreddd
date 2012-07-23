using EmailMaker.Infrastructure;
using NUnit.Framework;

namespace EmailMaker.IntegrationTests.DatabaseTests
{
    [SetUpFixture]
    public class RunOncePerTestRun
    {
        [SetUp]
        public void SetUp()
        {
            UnitOfWorkInitializer.Initialize();
        }
    }
}