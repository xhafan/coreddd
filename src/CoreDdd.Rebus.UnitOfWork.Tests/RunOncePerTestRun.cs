using IntegrationTestsShared;
using NUnit.Framework;

namespace CoreDdd.Rebus.UnitOfWork.Tests
{
    [SetUpFixture]
    public class RunOncePerTestRun : BaseRunOncePerTestRun
    {
        protected override string GetSynchronizationMutexName()
        {
            return GetType().Namespace;
        }
    }
}