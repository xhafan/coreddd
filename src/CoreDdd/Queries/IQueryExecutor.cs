using System.Collections.Generic;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Queries
{
    public interface IQueryExecutor
    {
        IEnumerable<TResult> Execute<TQuery, TResult>(TQuery query)
            where TQuery : IQuery;

#if !NET40
        Task<IEnumerable<TResult>> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery;

#endif
    }
}
