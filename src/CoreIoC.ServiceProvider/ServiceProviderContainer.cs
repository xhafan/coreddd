using Microsoft.Extensions.DependencyInjection;

namespace CoreIoC.ServiceProvider;

/// <summary>
/// Wrapper around Microsoft.Extensions.DependencyInjection ServiceProvider implementing <see cref="IContainer"/>.
/// </summary>
public class ServiceProviderContainer(IServiceProvider serviceProvider) : IContainer
{
    /// <summary>
    /// Resolve a service.
    /// </summary>
    /// <param name="serviceType">A service type</param>
    /// <returns>A service implementation</returns>
    public object Resolve(Type serviceType)
    {
        return serviceProvider.GetRequiredService(serviceType);
    }

    /// <summary>
    /// Resolve a service.
    /// </summary>
    /// <typeparam name="TService">A service type</typeparam>
    /// <returns>A service implementation</returns>
    public TService Resolve<TService>() where TService : notnull
    {
        return serviceProvider.GetRequiredService<TService>();
    }

    /// <summary>
    /// Resolve a service by a name (uses keyed services).
    /// </summary>
    /// <param name="name">A service name</param>
    /// <typeparam name="TService">A service type</typeparam>
    /// <returns>A service implementation</returns>
    public TService Resolve<TService>(string name) where TService : notnull
    {
        return serviceProvider.GetRequiredKeyedService<TService>(name);
    }

    /// <summary>
    /// Resolve all services for a given service type.
    /// </summary>
    /// <typeparam name="TService">A service type</typeparam>
    /// <returns>A collection of service implementations</returns>
    public IEnumerable<TService> ResolveAll<TService>()
    {
        return serviceProvider.GetServices<TService>();
    }

    /// <summary>
    /// Release a previously resolved service implementation. No-op for ServiceProvider (DI container manages lifetime).
    /// </summary>
    public void Release(object service)
    {
    }
}
