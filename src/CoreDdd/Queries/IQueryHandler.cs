using System.Collections.Generic;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Queries
{
    public interface IQueryHandler<in TQuery> 
        where TQuery : IQuery
    {
        IEnumerable<TResult> Execute<TResult>(TQuery query);

#if !NET40
        Task<IEnumerable<TResult>> ExecuteAsync<TResult>(TQuery query);
#endif
    }
}