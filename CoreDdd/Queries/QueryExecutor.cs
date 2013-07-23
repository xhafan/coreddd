using System;
using System.Collections.Generic;
using System.Linq;
using CoreIoC;

namespace CoreDdd.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        public IEnumerable<TResult> Execute<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery
        {
            var queryHandler = IoC.Resolve<IQueryHandler<TQuery>>();
            return queryHandler.Execute<TResult>(query);
        }

        public IEnumerable<TTransformResult> Execute<TQuery, TResult, TTransformResult>(TQuery query, Func<TResult, TTransformResult> transform) 
            where TQuery : IQuery
        {
            var queryHandler = IoC.Resolve<IQueryHandler<TQuery>>();
            var result = queryHandler.Execute<TResult>(query);
            return result.Select(transform);
        }
    }
}