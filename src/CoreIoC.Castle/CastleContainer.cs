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

        public object Resolve(Type serviceType)
        {
            return _windsorContainer.Resolve(serviceType);
        }

        public TService Resolve<TService>()
        {
            return _windsorContainer.Resolve<TService>();
        }

        public IEnumerable<TService> ResolveAll<TService>()
        {
            return _windsorContainer.ResolveAll<TService>();
        }

        public void Release(object service)
        {
            _windsorContainer.Release(service);
        }
    }
}