using Castle.Windsor;
using Castle.Windsor.Installer;
using CoreDdd.Nhibernate.Tests.Commands;
using IntegrationTestsShared;
using NUnit.Framework;

namespace CoreDdd.Nhibernate.Tests
{
    [SetUpFixture]
    public class RunOncePerTestRun : BaseRunOncePerTestRun
    {
        protected override string GetSynchronizationMutexName()
        {
            return GetType().Namespace;
        }

        protected override void RegisterAdditionalServices(WindsorContainer container)
        {
            container.Install(
                FromAssembly.Containing<TestCommandHandlerInstaller>()
            );
        }
    }
}