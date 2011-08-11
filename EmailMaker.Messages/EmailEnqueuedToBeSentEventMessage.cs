using System;
using NServiceBus;

namespace EmailMaker.Messages
{
    [Serializable]
    public class EmailEnqueuedToBeSentEventMessage : IMessage
    {
        public int EmailId { get; set; }
    }
}
