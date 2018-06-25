using System.Collections.Generic;

namespace CoreDdd.Domain.Events
{
    public class DelayedDomainEventHandlingItems : Queue<DelayedDomainEventHandlingItem>
    {
    }
}