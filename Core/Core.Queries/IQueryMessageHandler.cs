using System.Collections.Generic;

namespace Core.Queries
{
    public interface IQueryMessageHandler<in TQueryMessage> where TQueryMessage : IQueryMessage
    {
        IEnumerable<TResult> Handle<TResult>(TQueryMessage queryMessage);
    }
}