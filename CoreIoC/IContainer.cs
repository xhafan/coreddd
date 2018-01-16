using System;

namespace CoreIoC
{
    public interface IContainer
    {
        object Resolve(Type service);
        T Resolve<T>();
        T[] ResolveAll<T>();
    }
}