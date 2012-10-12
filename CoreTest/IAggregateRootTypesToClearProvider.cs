using System;
using System.Collections.Generic;

namespace CoreTest
{
    public interface IAggregateRootTypesToClearProvider
    {
        IEnumerable<Type> GetAggregateRootTypesToClear();
    }
}