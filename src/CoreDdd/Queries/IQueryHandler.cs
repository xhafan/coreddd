using System.Collections.Generic;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Queries
{
    /// <summary>
    /// Represents a query handler. Implement this interface on your query handler.
    /// </summary>
    /// <typeparam name="TQuery">A query type</typeparam>
    public interface IQueryHandler<in TQuery> 
        where TQuery : IQuery
    {
#if NET40
        /// <summary>
        /// Executes a query handling logic for a given query.
        /// </summary>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        IEnumerable<TResult> Execute<TResult>(TQuery query);
#endif

#if !NET40
        /// <summary>
        /// Executes a query handling logic asynchronously for a given query.
        /// </summary>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        Task<IEnumerable<TResult>> ExecuteAsync<TResult>(TQuery query);
#endif
    }
}