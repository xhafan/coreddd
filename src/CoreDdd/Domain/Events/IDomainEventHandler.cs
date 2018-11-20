namespace CoreDdd.Domain.Events
{
    /// <summary>
    /// Implement this interface for a domain event handler.
    /// </summary>
    /// <typeparam name="TDomainEvent">A domain event type.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Implement domain event handling logic here.
        /// </summary>
        /// <param name="domainEvent">A domain event type.</param>
        void Handle(TDomainEvent domainEvent);
    }
}