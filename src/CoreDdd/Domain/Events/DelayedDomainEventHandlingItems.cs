using System.Collections.Generic;

namespace CoreDdd.Domain.Events
{
    internal class DelayedDomainEventHandlingItems : Queue<DelayedDomainEventHandlingItem>
    {
    }
}