using System.Collections.Generic;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Queries
{
    /// <summary>
    /// Executes a query handling logic for a given query.
    /// </summary>
    public interface IQueryExecutor
    {
        /// <summary>
        /// Executes a query handling logic for a given query.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        IEnumerable<TResult> Execute<TQuery, TResult>(TQuery query)
            where TQuery : IQuery;

#if !NET40
        /// <summary>
        /// Executes a query handling logic asynchronously for a given query.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        Task<IEnumerable<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery;

#endif
    }
}
