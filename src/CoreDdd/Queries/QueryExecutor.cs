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

        public QueryExecutor(IQueryHandlerFactory queryHandlerFactory)
        {
            _queryHandlerFactory = queryHandlerFactory;
        }

        /// <summary>
        /// Executes a query handler based on the query type.
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

#if !NET40
        /// <summary>
        /// Executes a query handler asynchronously based on the query type.
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
#endif
    }
}