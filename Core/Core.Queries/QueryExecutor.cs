using System.Collections.Generic;
using Core.Commons;

namespace Core.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        public IEnumerable<TResult> Execute<TQueryMessage, TResult>(TQueryMessage queryMessage) where TQueryMessage : IQueryMessage
        {
            var queryHandler = IoC.Resolve<IQueryMessageHandler<TQueryMessage>>();
            return queryHandler.Execute<TResult>(queryMessage);
        }
    }
}