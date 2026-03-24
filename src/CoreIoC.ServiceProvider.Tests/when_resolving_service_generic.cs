#nullable enable
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using System;

namespace CoreIoC.ServiceProvider.Tests;

[TestFixture]
public class when_resolving_service_generic
{
    private interface IServiceType;
    protected class ServiceType : IServiceType;

    private object _result = null!;
    private IServiceProvider _serviceProvider = null!;

    [SetUp]
    public void Context()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IServiceType, ServiceType>();
        _serviceProvider = serviceCollection.BuildServiceProvider();


        var serviceProviderContainer = new ServiceProviderContainer(_serviceProvider);

        _result = serviceProviderContainer.Resolve<IServiceType>();
    }

    [Test]
    public void service_is_resolved()
    {
        _result.ShouldBeOfType<ServiceType>();
    }
}