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

        public object Resolve(Type service)
        {
            return _kernel.Get(service);
        }

        public T Resolve<T>()
        {
            return _kernel.Get<T>();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _kernel.GetAll<T>();
        }
    }
}
