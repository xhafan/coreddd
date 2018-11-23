using System;
using System.Collections.Generic;
using Castle.Windsor;

namespace CoreIoC.Castle
{
    /// <summary>
    /// Wrapper around Castle Windsor IoC container.
    /// </summary>
    public class CastleContainer : IContainer
    {
        private static IWindsorContainer _windsorContainer;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="windsorContainer">Castle Windsor container</param>
        public CastleContainer(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }

        /// <summary>
        /// Resolve a service.
        /// </summary>
        /// <param name="serviceType">A service type</param>
        /// <returns>A service implementation</returns>
        public object Resolve(Type serviceType)
        {
            return _windsorContainer.Resolve(serviceType);
        }

        /// <summary>
        /// Resolve a service.
        /// </summary>
        /// <typeparam name="TService">A service type</typeparam>
        /// <returns>A service implementation</returns>
        public TService Resolve<TService>()
        {
            return _windsorContainer.Resolve<TService>();
        }

        /// <summary>
        /// Resolve all services for a given service type.
        /// </summary>
        /// <typeparam name="TService">A service type</typeparam>
        /// <returns>A collection of service implementations</returns>
        public IEnumerable<TService> ResolveAll<TService>()
        {
            return _windsorContainer.ResolveAll<TService>();
        }

        /// <summary>
        /// Release a previously resolved service implementation.
        /// </summary>
        /// <param name="service">A service implementation previously resolved</param>
        public void Release(object service)
        {
            _windsorContainer.Release(service);
        }
    }
}