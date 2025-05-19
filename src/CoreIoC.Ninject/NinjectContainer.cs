using System;
using System.Collections.Generic;
using Ninject;

namespace CoreIoC.Ninject;

/// <summary>
/// Wrapper around Ninject IoC container.
/// </summary>
public class NinjectContainer : IContainer
{
    private readonly IKernel _kernel;

    /// <summary>
    /// Initializes the instance.
    /// </summary>
    /// <param name="kernel">Ninject kernel</param>
    public NinjectContainer(IKernel kernel)
    {
        _kernel = kernel;
    }

    /// <summary>
    /// Resolve a service.
    /// </summary>
    /// <param name="serviceType">A service type</param>
    /// <returns>A service implementation</returns>
    public object Resolve(Type serviceType)
    {
        return _kernel.Get(serviceType);
    }

    /// <summary>
    /// Resolve a service.
    /// </summary>
    /// <typeparam name="TService">A service type</typeparam>
    /// <returns>A service implementation</returns>
    public TService Resolve<TService>()
    {
        return _kernel.Get<TService>();
    }

    /// <summary>
    /// Resolve all services for a given service type.
    /// </summary>
    /// <typeparam name="TService">A service type</typeparam>
    /// <returns>A collection of service implementations</returns>
    public IEnumerable<TService> ResolveAll<TService>()
    {
        return _kernel.GetAll<TService>();
    }

    /// <summary>
    /// Release a previously resolved service implementation.
    /// </summary>
    /// <param name="service">A service implementation previously resolved</param>
    public void Release(object service)
    {
        _kernel.Release(service);
    }
}