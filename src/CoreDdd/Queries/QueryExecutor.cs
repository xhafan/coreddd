using System.Collections.Generic;
using CoreIoC;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        public IEnumerable<TResult> Execute<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery
        {
            var queryHandler = IoC.Resolve<IQueryHandler<TQuery>>();

            try
            {
                return queryHandler.Execute<TResult>(query);
            }
            finally
            {
                IoC.Release(queryHandler);
            }
        }

#if !NET40
        public Task<IEnumerable<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery
        {
            var queryHandler = IoC.Resolve<IQueryHandler<TQuery>>();

            try
            {
                return queryHandler.ExecuteAsync<TResult>(query);
            }
            finally
            {
                IoC.Release(queryHandler);
            }
        }
#endif
    }
}