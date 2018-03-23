using System;
using System.Collections.Generic;

namespace CoreIoC
{
    public interface IContainer
    {
        object Resolve(Type service);
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
    }
}