using System;
using Castle.Windsor;

namespace CoreIoC
{
    public static class IoC
    {
        private static IWindsorContainer _container;

        public static void Initialize(IWindsorContainer container)
        {
            _container = container;
        }
        
        public static object Resolve(Type service)
        {
            return _container.Resolve(service);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public static void Release(object instance)
        {
            _container.Release(instance);
        }
    
    }
}
