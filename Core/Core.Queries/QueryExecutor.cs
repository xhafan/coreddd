using System.Collections.Generic;
using CoreIoC;

namespace Core.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        public IEnumerable<TResult> Execute<TQueryMessage, TResult>(TQueryMessage queryMessage) where TQueryMessage : IQueryMessage
        {
            var queryHandler = IoC.Resolve<IQueryMessageHandler<TQueryMessage>>();
            return queryHandler.Handle<TResult>(queryMessage);
        }
    }
}