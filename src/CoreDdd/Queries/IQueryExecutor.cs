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
        /// Executes a query handling logic for a given query and returns a collection of results.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        IEnumerable<TResult> Execute<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
        
        /// <summary>
        /// Executes a query handling logic for a given query and returns a single result.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>
        TResult ExecuteSingle<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;   

#if !NET40
        /// <summary>
        /// Executes a query handling logic asynchronously for a given query and returns a collection of results.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        Task<IEnumerable<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
        
        /// <summary>
        /// Executes a query handling logic asynchronously for a given query and returns a single result.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>        
        Task<TResult> ExecuteSingleAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;     
#endif
    }
}
