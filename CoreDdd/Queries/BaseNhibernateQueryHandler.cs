using System.Collections.Generic;
using CoreDdd.Infrastructure;
using NHibernate;

namespace CoreDdd.Queries
{
    public abstract class BaseNhibernateQueryHandler<TQuery> : IQueryHandler<TQuery> where TQuery : IQuery
    {
        protected ISession Session;

        protected BaseNhibernateQueryHandler()
        {
            Session = UnitOfWork.CurrentSession;
        }

        public abstract IEnumerable<TResult> Execute<TResult>(TQuery query);
    }
}