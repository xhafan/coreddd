using Core.Utilities;

namespace EmailMaker.Utilities
{
    public class EmailMakerException : CoreException
    {
        public EmailMakerException(string message)
            : base(message)
        {
        }
    }
}