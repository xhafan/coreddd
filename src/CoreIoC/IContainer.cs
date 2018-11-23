using System;
using System.Collections.Generic;

namespace CoreIoC
{
    /// <summary>
    /// Represents an IoC container.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Resolve a service.
        /// </summary>
        /// <param name="serviceType">A service type</param>
        /// <returns>A service implementation</returns>
        object Resolve(Type serviceType);

        /// <summary>
        /// Resolve a service.
        /// </summary>
        /// <typeparam name="TService">A service type</typeparam>
        /// <returns>A service implementation</returns>
        TService Resolve<TService>();

        /// <summary>
        /// Resolve all services for a given service type.
        /// </summary>
        /// <typeparam name="TService">A service type</typeparam>
        /// <returns>A collection of service implementations</returns>
        IEnumerable<TService> ResolveAll<TService>();

        /// <summary>
        /// Release a previously resolved service implementation.
        /// </summary>
        /// <param name="service">A service implementation previously resolved</param>
        void Release(object service);
    }
}