using System.Collections.Generic;

namespace CoreDdd.Queries
{
    public interface IQueryExecutor
    {
        IEnumerable<TResult> Execute<TQuery, TResult>(TQuery query)
            where TQuery : IQuery;
    }
}
