using Core.Utilities;

namespace EmailMaker.Core
{
    public class EmailMakerException : CoreException
    {
        public EmailMakerException(string message)
            : base(message)
        {
        }
    }
}