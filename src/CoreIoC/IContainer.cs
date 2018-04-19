using System;
using System.Collections.Generic;

namespace CoreIoC
{
    public interface IContainer
    {
        object Resolve(Type serviceType);
        TService Resolve<TService>();
        IEnumerable<TService> ResolveAll<TService>();
    }
}