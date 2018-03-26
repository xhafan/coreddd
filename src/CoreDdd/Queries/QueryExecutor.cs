using System.Collections.Generic;
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
    }
}