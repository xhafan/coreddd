using System;
using System.Collections.Generic;

namespace CoreDdd.Queries
{
    public interface IQueryExecutor
    {
        IEnumerable<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery;
        IEnumerable<TTransformResult> Execute<TQuery, TResult, TTransformResult>(TQuery query, Func<TResult, TTransformResult> transform) where TQuery : IQuery;
    }
}
