using System.Collections.Generic;
using NHibernate;

namespace Core.Queries
{
    public abstract class BaseNHibernateCriteriaQueryMessageHandler<TQueryMessage> : BaseNHibernateQueryMessageHandler<TQueryMessage> where TQueryMessage : IQueryMessage
    {
        public abstract ICriteria GetCriteria<TResult>(TQueryMessage message);

        public override IEnumerable<TResult> Execute<TResult>(TQueryMessage message)
        {
            return GetCriteria<TResult>(message).Future<TResult>();
        }
    }
}