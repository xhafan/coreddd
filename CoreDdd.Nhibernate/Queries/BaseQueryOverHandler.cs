using System.Collections.Generic;
using NHibernate;
using IQuery = CoreDdd.Queries.IQuery;

namespace CoreDdd.Nhibernate.Queries
{
    public abstract class BaseQueryOverHandler<TQuery> : BaseNhibernateQueryHandler<TQuery> where TQuery : IQuery
    {
        public abstract IQueryOver GetQueryOver<TResult>(TQuery query);

        public override IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            return GetQueryOver<TResult>(query).UnderlyingCriteria.Future<TResult>();
        }
    }
}