using System;
using System.Collections.Generic;
using System.Linq;
using Core.Infrastructure;

namespace Core.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        public IEnumerable<TResult> Execute<TQueryMessage, TResult>(TQueryMessage queryMessage) 
            where TQueryMessage : IQueryMessage
        {
            var queryHandler = IoC.Resolve<IQueryMessageHandler<TQueryMessage>>();
            return queryHandler.Execute<TResult>(queryMessage);
        }

        public IEnumerable<TTransformResult> Execute<TQueryMessage, TResult, TTransformResult>(TQueryMessage queryMessage, Func<TResult, TTransformResult> transform) 
            where TQueryMessage : IQueryMessage
        {
            var queryHandler = IoC.Resolve<IQueryMessageHandler<TQueryMessage>>();
            var result = queryHandler.Execute<TResult>(queryMessage);
            return result.Select(transform);
        }
    }
}