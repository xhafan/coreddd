using System;
using IntegrationTestsShared;
using NUnit.Framework;

namespace CoreDdd.AspNet.Tests;

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
}