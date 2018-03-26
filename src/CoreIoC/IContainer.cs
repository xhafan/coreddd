using System;
using System.Collections.Generic;

namespace CoreIoC
{
    public interface IContainer
    {
        object Resolve(Type service);
        TService Resolve<TService>();
        IEnumerable<TService> ResolveAll<TService>();
    }
}