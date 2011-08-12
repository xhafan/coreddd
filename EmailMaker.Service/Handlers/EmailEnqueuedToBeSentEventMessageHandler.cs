using System;
using EmailMaker.Messages;
using NServiceBus;

namespace EmailMaker.Service.Handlers
{
    public class EmailEnqueuedToBeSentEventMessageHandler : IMessageHandler<EmailEnqueuedToBeSentEventMessage>
    {
        public void Handle(EmailEnqueuedToBeSentEventMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
