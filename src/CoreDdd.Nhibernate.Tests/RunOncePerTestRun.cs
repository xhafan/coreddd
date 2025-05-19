using Castle.Windsor;
using Castle.Windsor.Installer;
using CoreDdd.Nhibernate.Tests.Commands;
using IntegrationTestsShared;
using NUnit.Framework;
using System;

namespace CoreDdd.Nhibernate.Tests;

[SetUpFixture]
public class RunOncePerTestRun : BaseRunOncePerTestRun
{
    protected override string GetSynchronizationMutexName()
    {
        var synchronizationMutexName = GetType().Namespace;
        if (synchronizationMutexName == null)
        {
            throw new Exception($"{nameof(synchronizationMutexName)} is null");
        }
        return synchronizationMutexName;
    }

    protected override void RegisterAdditionalServices(WindsorContainer container)
    {
        container.Install(
            FromAssembly.Containing<TestCommandHandlerInstaller>()
        );
    }
}