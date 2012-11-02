using System.Collections.Generic;
using NHibernate;

namespace CoreDdd.Queries
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