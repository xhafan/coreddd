using System;

namespace CoreDdd.Utilities
{
    public class CoreException : Exception
    {
        public CoreException(string message) : base(message)
        {
        }
    }
}