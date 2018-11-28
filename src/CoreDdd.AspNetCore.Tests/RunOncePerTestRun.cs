using IntegrationTestsShared;
using NUnit.Framework;

namespace CoreDdd.AspNetCore.Tests
{
    [SetUpFixture]
    public class RunOncePerTestRun : BaseRunOncePerTestRun
    {
        protected override string GetSychronizationMutexName()
        {
            return GetType().Namespace;
        }
    }
}