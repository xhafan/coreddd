#nullable enable
using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace CoreIoC.ServiceProvider.Tests;

[TestFixture]
public class when_resolving_service_generic_by_a_name
{
    private interface IServiceType;
    protected class ServiceType : IServiceType;

    private object _result = null!;
    private IServiceProvider _serviceProvider = null!;

    [SetUp]
    public void Context()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddKeyedTransient<IServiceType, ServiceType>("name1");
        _serviceProvider = serviceCollection.BuildServiceProvider();


        var serviceProviderContainer = new ServiceProviderContainer(_serviceProvider);

        _result = serviceProviderContainer.Resolve<IServiceType>("name1");
    }

    [Test]
    public void service_is_resolved()
    {
        _result.ShouldBeOfType<ServiceType>();
    }
}