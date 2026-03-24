#nullable enable
using NUnit.Framework;
using Shouldly;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace CoreIoC.ServiceProvider.Tests;

[TestFixture]
public class when_resolving_service
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

        _result = serviceProviderContainer.Resolve(typeof(IServiceType));
    }

    [Test]
    public void service_is_resolved()
    {
        _result.ShouldBeOfType<ServiceType>();
    }
}