using System;
using System.Collections.Generic;

namespace CoreIoC
{
    public static class IoC
    {
        private static IContainer _container;

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

        public static object Resolve(Type service)
        {
            return _getContainer().Resolve(service);
        }

        public static TService Resolve<TService>()
        {
            return _getContainer().Resolve<TService>();
        }

        public static IEnumerable<TService> ResolveAll<TService>()
        {
            return _getContainer().ResolveAll<TService>();
        }
    }
}
