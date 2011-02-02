using System.Collections.Generic;
using Core.Commons;
using NHibernate;

namespace Core.Queries
{
    public abstract class BaseNHibernateQueryMessageHandler<TQueryMessage> : IQueryMessageHandler<TQueryMessage> where TQueryMessage : IQueryMessage
    {
        protected ISession Session;

        protected BaseNHibernateQueryMessageHandler()
        {
            Session = UnitOfWork.CurrentSession;
        }

        public abstract IEnumerable<TResult> Execute<TResult>(TQueryMessage message);
    }
}