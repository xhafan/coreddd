using System.Collections.Generic;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.Queries;
using CoreIoC;
using NHibernate;
using IQuery = CoreDdd.Queries.IQuery;

namespace CoreDdd.Nhibernate.Queries
{
    public abstract class BaseNhibernateQueryHandler<TQuery> : IQueryHandler<TQuery> where TQuery : IQuery
    {
        protected ISession Session;

        protected BaseNhibernateQueryHandler()
        {
            Session = IoC.Resolve<NhibernateUnitOfWork>().Session;
        }

        public abstract IEnumerable<TResult> Execute<TResult>(TQuery query);
    }
}