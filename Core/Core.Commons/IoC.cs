using System;
using Castle.Windsor;

namespace Core.Commons
{
    public static class IoC
    {
        private static IWindsorContainer _container;

        public static void Initialize(IWindsorContainer container)
        {
            _container = container;
        }

        private static IWindsorContainer Container
        {
            get
            {
                if (_container == null)
                {
                    throw new InvalidOperationException("The container has not been initialized! Please call IoC.Initialize(container) before using it.");
                }
                return _container;
            }
        }
        
        public static object Resolve(Type service)
        {
            return Container.Resolve(service);
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static void Release(object instance)
        {
            Container.Release(instance);
        }
    
    }
}
