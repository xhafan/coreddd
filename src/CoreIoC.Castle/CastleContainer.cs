using System;
using System.Collections.Generic;
using Castle.Windsor;

namespace CoreIoC.Castle
{
    public class CastleContainer : IContainer
    {
        private static IWindsorContainer _windsorContainer;

        public CastleContainer(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer;
        }

        public object Resolve(Type service)
        {
            return _windsorContainer.Resolve(service);
        }

        public T Resolve<T>()
        {
            return _windsorContainer.Resolve<T>();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _windsorContainer.ResolveAll<T>();
        }
    }
}