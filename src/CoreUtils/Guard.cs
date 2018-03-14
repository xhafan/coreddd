using System;

namespace CoreUtils
{
    public static class Guard
    {
        public static void Hope(bool condition, string message)
        {
            Hope<Exception>(condition, message);
        }

        public static void Hope<TException>(bool condition, string message)
            where TException : Exception
        {
            if (condition) return;

            var exception = (TException)Activator.CreateInstance(typeof(TException), message);
            throw exception;
        }
    }
}
