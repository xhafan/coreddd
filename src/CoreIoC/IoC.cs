using System;
using System.Collections.Generic;

namespace CoreIoC
{
    /// <summary>
    /// Service locator. Use only when a dependency injection provided by an IoC container is not available, like for ASP.NET Web Forms.
    /// </summary>
    public static class IoC
    {
        private static IContainer? _container;

        /// <summary>
        /// Initializes the service locator. Must be called at an application start.
        /// </summary>
        /// <param name="container">An IoC container</param>
        public static void Initialize(IContainer container)
        {
            _container = container;
        }

        private static IContainer _getContainer()
        {
            if (_container == null)
            {
                throw new InvalidOperationException(
                    "The container has not been initialized! Please call IoC.Initialize(container) before using it.");
            }

            return _container;
        }
        
        /// <summary>
        /// Checks if service locator is initialized.
        /// </summary>
        /// <returns>True - service locator is initialized by an IoC container; False - otherwise</returns>
        public static bool IsInitialized()
        {
            return _container != null;
        }        

        /// <summary>
        /// Resolve a service.
        /// </summary>
        /// <param name="serviceType">A service type</param>
        /// <returns>A service implementation</returns>
        public static object Resolve(Type serviceType)
        {
            return _getContainer().Resolve(serviceType);
        }

        /// <summary>
        /// Resolve a service.
        /// </summary>
        /// <typeparam name="TService">A service type</typeparam>
        /// <returns>A service implementation</returns>
        public static TService Resolve<TService>()
        {
            return _getContainer().Resolve<TService>();
        }

        /// <summary>
        /// Resolve all services for a given service type.
        /// </summary>
        /// <typeparam name="TService">A service type</typeparam>
        /// <returns>A collection of service implementations</returns>
        public static IEnumerable<TService> ResolveAll<TService>()
        {
            return _getContainer().ResolveAll<TService>();
        }

        /// <summary>
        /// Release a previously resolved service implementation.
        /// </summary>
        /// <param name="service">A service implementation previously resolved</param>
        public static void Release(object service)
        {
            _getContainer().Release(service);
        }
        
        /// <summary>
        /// Uninitialize the service locator. Should be called at an application end.
        /// </summary>
        public static void Uninitialize()
        {
            _container = null;
        }        
    }
}
