using System.Collections.Generic;

namespace CoreDdd.Queries
{
    public interface IQueryMessageHandler<in TQueryMessage> where TQueryMessage : IQueryMessage
    {
        IEnumerable<TResult> Execute<TResult>(TQueryMessage message);
    }
}