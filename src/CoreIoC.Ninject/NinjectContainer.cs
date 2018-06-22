using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Syntax;

namespace CoreIoC.Ninject
{
    public class NinjectContainer : IContainer
    {
        private static IResolutionRoot _kernel;

        public NinjectContainer(IResolutionRoot kernel)
        {
            _kernel = kernel;
        }

        public object Resolve(Type serviceType)
        {
            return _kernel.Get(serviceType);
        }

        public TService Resolve<TService>()
        {
            return _kernel.Get<TService>();
        }

        public IEnumerable<TService> ResolveAll<TService>()
        {
            return _kernel.GetAll<TService>();
        }

        public void Release(object service)
        {
            _kernel.Release(service);
        }
    }
}
