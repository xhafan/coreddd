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
    /// <typeparam name="TResult">A query result type</typeparam>
    public interface IQueryHandler<in TQuery, TResult> 
        where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Executes a query handling logic for a given query and returns a collection of results.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        IEnumerable<TResult> Execute(TQuery query);

        /// <summary>
        /// Executes a query handling logic for a given query and returns a single result.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>
        TResult ExecuteSingle(TQuery query);

#if !NET40
        /// <summary>
        /// Executes a query handling logic asynchronously for a given query and returns a collection of results.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        Task<IEnumerable<TResult>> ExecuteAsync(TQuery query);

        /// <summary>
        /// Executes a query handling logic asynchronously for a given query and returns a single result.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>        
        Task<TResult> ExecuteSingleAsync(TQuery query);
#endif
    }
}