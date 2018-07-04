using System.Collections.Generic;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IQueryHandlerFactory _queryHandlerFactory;

        public QueryExecutor(IQueryHandlerFactory queryHandlerFactory)
        {
            _queryHandlerFactory = queryHandlerFactory;
        }

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