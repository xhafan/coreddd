using NServiceBus;

namespace EmailMaker.Messages
{
    public class EmailEnqueuedToBeSentEventMessage : IMessage
    {
        public int EmailId { get; set; }
    }
}
