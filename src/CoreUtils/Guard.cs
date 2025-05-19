using System;
using System.Collections;

#if NETSTANDARD2_1_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
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
        public static void Hope(
#if NETSTANDARD2_1_OR_GREATER
            [DoesNotReturnIf(false)] // this ensures that when the nullable reference types are enabled, one does not get "Dereference of possibly null reference" warning (idea taken from Debug.Assert method)
#endif
            bool condition, 
            string message
            )
        {
            Hope<Exception>(condition, message, data: null);
        }
        
        /// <summary>
        /// Hopes the condition is true, and if not throws an exception.
        /// </summary>
        /// <param name="condition">A condition</param>
        /// <param name="message">An exception message</param>
        /// <param name="data">An exception data (optional)</param>
        [ContractAnnotation("condition: false => halt")]
        public static void Hope(
#if NETSTANDARD2_1_OR_GREATER
            [DoesNotReturnIf(false)] // this ensures that when the nullable reference types are enabled, one does not get "Dereference of possibly null reference" warning (idea taken from Debug.Assert method)
#endif
            bool condition, 
            string message,
            IDictionary data
        )
        {
            Hope<Exception>(condition, message, data);
        } 

        /// <summary>
        /// Hopes the condition is true, and if not throws an exception of a given type.
        /// </summary>
        /// <typeparam name="TException">An exception type</typeparam>
        /// <param name="condition">A condition</param>
        /// <param name="message">An exception message</param>
        [ContractAnnotation("condition: false => halt")]
        public static void Hope<TException>(
#if NETSTANDARD2_1_OR_GREATER
            [DoesNotReturnIf(false)] // this ensures that when the nullable reference types are enabled, one does not get "Dereference of possibly null reference" warning (idea taken from Debug.Assert method)
#endif
            bool condition, 
            string message
        )
            where TException : Exception
        {
            Hope<TException>(condition, message, data: null);
        }        

        /// <summary>
        /// Hopes the condition is true, and if not throws an exception of a given type.
        /// </summary>
        /// <typeparam name="TException">An exception type</typeparam>
        /// <param name="condition">A condition</param>
        /// <param name="message">An exception message</param>
        /// <param name="data">An exception data (optional)</param>
        [ContractAnnotation("condition: false => halt")]
        public static void Hope<TException>(
#if NETSTANDARD2_1_OR_GREATER
            [DoesNotReturnIf(false)] // this ensures that when the nullable reference types are enabled, one does not get "Dereference of possibly null reference" warning (idea taken from Debug.Assert method)
#endif
            bool condition, 
            string message,
            IDictionary? data
            )
            where TException : Exception
        {
            if (condition) return;

            var exception = (TException)Activator.CreateInstance(typeof(TException), message);
            if (data != null)
            {
                foreach (DictionaryEntry entry in data)
                {
                    exception.Data.Add(entry.Key, entry.Value);
                }
            }
            throw exception;
        }
    }
}
