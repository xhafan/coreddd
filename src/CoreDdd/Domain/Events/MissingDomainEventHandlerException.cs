using System;

namespace CoreDdd.Domain.Events
{
    public class MissingDomainEventHandlerException : Exception
    {
        public MissingDomainEventHandlerException(string message) : base(message)
        {
        }
    }
}