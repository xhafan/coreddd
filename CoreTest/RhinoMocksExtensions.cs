using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks
{
    // Chaining supports for Stub / Return
    public static class RhinoMocksExtensions
    {
        public static (T Target, IMethodOptions<TReturn> MethodOptions) Stubs<T, TReturn>(this T target, Function<T, TReturn> action)
                   where T : class
        {
            return (target, MethodOptions: target.Stub(action));
        }

        public static T Returns<T, TReturn>(this (T Target, IMethodOptions<TReturn> MethodOptions) targetAndMethodOptions, TReturn objToReturn)
            where T : class
        {
            targetAndMethodOptions.MethodOptions.Return(objToReturn);
            return targetAndMethodOptions.Target;
        }
    }
}
