using System.Collections.Generic;

namespace Core.Queries
{
    public interface IQueryExecutor
    {
        IEnumerable<TResult> Execute<TQueryMessage, TResult>(TQueryMessage queryMessage) where TQueryMessage : IQueryMessage;
    }
}
