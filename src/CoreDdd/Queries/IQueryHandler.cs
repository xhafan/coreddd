using System.Collections.Generic;

namespace CoreDdd.Queries
{
    public interface IQueryHandler<in TQuery> where TQuery : IQuery
    {
        IEnumerable<TResult> Execute<TResult>(TQuery query);
    }
}