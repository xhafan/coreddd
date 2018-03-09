using System;

namespace CoreUtils
{
    public class CoreException : Exception
    {
        public CoreException(string message) : base(message)
        {
        }
    }
}