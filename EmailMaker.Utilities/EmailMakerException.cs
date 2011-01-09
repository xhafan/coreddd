using System;

namespace EmailMaker.Utilities
{
    public class EmailMakerException : Exception
    {
        public EmailMakerException(string message) : base(message)
        {
        }
    }
}