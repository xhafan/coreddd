#nullable enable
using CoreUtils.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreIoC.ServiceProvider.Tests;

[TestFixture]
public class when_resolving_all_services_generic
{
    private interface IServiceType;
    protected class ServiceTypeOne : IServiceType;
    protected class ServiceTypeTwo : IServiceType;

    private IEnumerable<IServiceType> _result = null!;
    private IServiceProvider _serviceProvider = null!;

    [SetUp]
    public void Context()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IServiceType, ServiceTypeOne>();
        serviceCollection.AddTransient<IServiceType, ServiceTypeTwo>();
        _serviceProvider = serviceCollection.BuildServiceProvider();


        var serviceProviderContainer = new ServiceProviderContainer(_serviceProvider);

        _result = serviceProviderContainer.ResolveAll<IServiceType>();
    }

    [Test]
    public void all_service_are_resolved()
    {
        _result.Count().ShouldBe(2);
        _result.First().ShouldBeOfType<ServiceTypeOne>();
        _result.Second().ShouldBeOfType<ServiceTypeTwo>();
    }
}