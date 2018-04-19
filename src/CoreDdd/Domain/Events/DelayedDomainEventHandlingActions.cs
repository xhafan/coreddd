using System;
using System.Collections.Generic;

namespace CoreDdd.Domain.Events
{
    public class DelayedDomainEventHandlingActions : Queue<Action>
    {
    }
}