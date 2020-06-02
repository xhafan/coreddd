using System;
using JetBrains.Annotations;

namespace CoreUtils
{
    /// <summary>
    /// Guards a condition to be true, and if not throws an exception.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Hopes the condition is true, and if not throws an exception.
        /// </summary>
        /// <param name="condition">A condition</param>
        /// <param name="message">An exception message</param>
        [ContractAnnotation("condition: false => halt")]
        public static void Hope(bool condition, string message)
        {
            Hope<Exception>(condition, message);
        }

        /// <summary>
        /// Hopes the condition is true, and if not throws an exception of a given type.
        /// </summary>
        /// <typeparam name="TException">An exception type</typeparam>
        /// <param name="condition">A condition</param>
        /// <param name="message">An exception message</param>
        [ContractAnnotation("condition: false => halt")]
        public static void Hope<TException>(bool condition, string message)
            where TException : Exception
        {
            if (condition) return;

            var exception = (TException)Activator.CreateInstance(typeof(TException), message);
            throw exception;
        }
    }
}
