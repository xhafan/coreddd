using System;
using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks
{
    // Chaining supports for Stub / Return
    public static class RhinoMocksExtensions
    {
        public static Tuple<T, IMethodOptions<TReturn>> Stubs<T, TReturn>(this T target, Function<T, TReturn> action)
                   where T : class
        {
            return new Tuple<T, IMethodOptions<TReturn>>(target, target.Stub(action));
        }

        public static T Returns<T, TReturn>(this Tuple<T, IMethodOptions<TReturn>> targetAndMethodOptions, TReturn objToReturn)
            where T : class
        {
            targetAndMethodOptions.Item2.Return(objToReturn);
            return targetAndMethodOptions.Item1;
        }
    }
}
