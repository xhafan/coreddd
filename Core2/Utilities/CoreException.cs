using System;

namespace Core.Utilities
{
    public class CoreException : Exception
    {
        public CoreException(string message) : base(message)
        {
        }
    }
}