using System;
using System.Collections.Generic;

namespace CoreDdd.Queries
{
    public interface IQueryExecutor
    {
        IEnumerable<TResult> Execute<TQueryMessage, TResult>(TQueryMessage queryMessage) where TQueryMessage : IQueryMessage;
        IEnumerable<TTransformResult> Execute<TQueryMessage, TResult, TTransformResult>(TQueryMessage queryMessage, Func<TResult, TTransformResult> transform) where TQueryMessage : IQueryMessage;
    }
}
