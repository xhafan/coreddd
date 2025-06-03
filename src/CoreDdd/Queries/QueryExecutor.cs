using System.Collections.Generic;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Queries
{
    /// <summary>
    /// Instantiates a query handler for a given query, and executes it.
    /// </summary>
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IQueryHandlerFactory _queryHandlerFactory;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="queryHandlerFactory">A query handler factory</param>
        public QueryExecutor(IQueryHandlerFactory queryHandlerFactory)
        {
            _queryHandlerFactory = queryHandlerFactory;
        }

        /// <summary>
        /// Executes a query handling logic for a given query and returns a collection of results.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with data</param>
        /// <returns>A collection of query results</returns>
        public IEnumerable<TResult> Execute<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery
        {
            var queryHandler = _queryHandlerFactory.Create<TQuery>();

            try
            {
                return queryHandler.Execute<TResult>(query);
            }
            finally
            {
                _queryHandlerFactory.Release(queryHandler);
            }
        }

        /// <summary>
        /// Executes a query handling logic asynchronously for a given query and returns a single result.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>        
        public TResult ExecuteSingle<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            var queryHandler = _queryHandlerFactory.Create<TQuery>();

            try
            {
                return queryHandler.ExecuteSingle<TResult>(query);
            }
            finally
            {
                _queryHandlerFactory.Release(queryHandler);
            }
        }

#if !NET40
        /// <summary>
        /// Executes a query handling logic asynchronously for a given query and returns a collection of results.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with data</param>
        /// <returns>A collection of query results</returns>
        public Task<IEnumerable<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery
        {
            var queryHandler = _queryHandlerFactory.Create<TQuery>();

            try
            {
                return queryHandler.ExecuteAsync<TResult>(query);
            }
            finally
            {
                _queryHandlerFactory.Release(queryHandler);
            }
        }

        /// <summary>
        /// Executes a query handling logic asynchronously for a given query and returns a single result.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>            
        public Task<TResult> ExecuteSingleAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            var queryHandler = _queryHandlerFactory.Create<TQuery>();

            try
            {
                return queryHandler.ExecuteSingleAsync<TResult>(query);
            }
            finally
            {
                _queryHandlerFactory.Release(queryHandler);
            }
        }
#endif
    }
}