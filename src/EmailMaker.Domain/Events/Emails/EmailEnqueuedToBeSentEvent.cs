using CoreDdd.Domain.Events;

namespace EmailMaker.Domain.Events.Emails
{
    public class EmailEnqueuedToBeSentEvent : IDomainEvent
    {
        public int EmailId { get; set; }
    }
}
